import { useState, useEffect } from 'react'
import { useCart } from '../context/CartContext'
import { ordersApi } from '../api'
import styles from './CartModal.module.css'

interface Props {
  open: boolean
  onClose: () => void
}

export default function CartModal({ open, onClose }: Props) {
  const { items, removeItem, updateQty, clearCart, total } = useCart()
  const [step, setStep] = useState<'cart' | 'order' | 'success'>('cart')
  const [form, setForm] = useState({
    customerName: '', phone: '', email: '',
    address: '', comment: '', deliveryRequired: true
  })
  const [submitting, setSubmitting] = useState(false)
  const [orderId, setOrderId] = useState<number | null>(null)

  // Lock body scroll when open
  useEffect(() => {
    document.body.style.overflow = open ? 'hidden' : ''
    return () => { document.body.style.overflow = '' }
  }, [open])

  // Reset on close
  useEffect(() => {
    if (!open) {
      setTimeout(() => { setStep('cart') }, 300)
    }
  }, [open])

  if (!open) return null

  const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault()
  setSubmitting(true)
  try {
    const order = await ordersApi.create({
      customerName: form.customerName,
      phone: form.phone,
      email: form.email,
      address: form.deliveryRequired ? form.address : '',
      comment: form.comment,
      deliveryRequired: form.deliveryRequired,
      items: items.map(i => ({
        productId: i.product.id,
        quantity: i.quantity
      }))
    })
    setOrderId(order.id)
    setStep('success')
    clearCart()
  } catch (err: any) {
    // Обробка ValidationException з бекенду
    if (err.response?.status === 400) {
      const data = err.response.data
      if (data?.errors) {
        const messages = Object.values(data.errors).flat().join(', ')
        alert(`Помилка валідації: ${messages}`)
      } else {
        alert('Перевірте правильність заповнення форми')
      }
    } else {
      alert('Помилка сервера. Будь ласка, зателефонуйте нам.')
    }
  } finally {
    setSubmitting(false)
  }
}
  return (
    <div className={styles.overlay} onClick={onClose}>
      <div className={styles.modal} onClick={e => e.stopPropagation()}>

        {/* Header */}
        <div className={styles.header}>
          <div className={styles.headerLeft}>
            {step === 'order' && (
              <button className={styles.backBtn} onClick={() => setStep('cart')}>← Назад</button>
            )}
            <h2 className={styles.title}>
              {step === 'cart' && '🛒 Кошик'}
              {step === 'order' && '📋 Оформлення'}
              {step === 'success' && '✅ Замовлення прийнято'}
            </h2>
          </div>
          <button className={styles.closeBtn} onClick={onClose}>✕</button>
        </div>

        {/* CART STEP */}
        {step === 'cart' && (
          <div className={styles.body}>
            {items.length === 0 ? (
              <div className={styles.empty}>
                <span className={styles.emptyIcon}>🛒</span>
                <p>Кошик порожній</p>
                <button className={styles.shopBtn} onClick={onClose}>
                  Перейти до каталогу
                </button>
              </div>
            ) : (
              <>
                <div className={styles.itemsList}>
                  {items.map(({ product, quantity }) => {
                    const price = product.unit === 'м³' ? product.pricePerCubicMeter : product.pricePerPiece
                    return (
                      <div key={product.id} className={styles.item}>
                        <div className={styles.itemIcon}>🪵</div>
                        <div className={styles.itemInfo}>
                          <p className={styles.itemName}>{product.name}</p>
                          <p className={styles.itemPrice}>
                            {price.toLocaleString('uk-UA')} ₴ / {product.unit}
                          </p>
                        </div>
                        <div className={styles.itemQty}>
                          <button onClick={() => updateQty(product.id, quantity - 0.5)}>−</button>
                          <span>{quantity}</span>
                          <button onClick={() => updateQty(product.id, quantity + 0.5)}>+</button>
                        </div>
                        <div className={styles.itemTotal}>
                          {(price * quantity).toLocaleString('uk-UA')} ₴
                        </div>
                        <button className={styles.removeBtn} onClick={() => removeItem(product.id)}>×</button>
                      </div>
                    )
                  })}
                </div>

                <div className={styles.footer}>
                  <div className={styles.totalRow}>
                    <span>Разом:</span>
                    <strong>{total.toLocaleString('uk-UA')} ₴</strong>
                  </div>
                  <button className={styles.orderBtn} onClick={() => setStep('order')}>
                    Оформити замовлення →
                  </button>
                </div>
              </>
            )}
          </div>
        )}

        {/* ORDER STEP */}
        {step === 'order' && (
          <div className={styles.body}>
            <form className={styles.form} onSubmit={handleSubmit}>
              <label className={styles.label}>
                Ваше ім'я *
                <input className={styles.input} required value={form.customerName}
                  onChange={e => setForm(p => ({ ...p, customerName: e.target.value }))} />
              </label>
              <label className={styles.label}>
                Телефон *
                <input className={styles.input} type="tel" required value={form.phone}
                  placeholder="+38 (0__) ___-__-__"
                  onChange={e => setForm(p => ({ ...p, phone: e.target.value }))} />
              </label>
              <label className={styles.label}>
                Email
                <input className={styles.input} type="email" value={form.email}
                  onChange={e => setForm(p => ({ ...p, email: e.target.value }))} />
              </label>

              <label className={styles.toggle}>
                <input type="checkbox" checked={form.deliveryRequired}
                  onChange={e => setForm(p => ({ ...p, deliveryRequired: e.target.checked }))} />
                <span>🚚 Потрібна доставка по Рівному</span>
              </label>

              {form.deliveryRequired && (
                <label className={styles.label}>
                  Адреса доставки *
                  <input className={styles.input} required={form.deliveryRequired}
                    placeholder="вул. Назва, буд. Х"
                    value={form.address}
                    onChange={e => setForm(p => ({ ...p, address: e.target.value }))} />
                </label>
              )}

              <label className={styles.label}>
                Коментар
                <textarea className={`${styles.input} ${styles.textarea}`} rows={3}
                  placeholder="Час доставки, особливі побажання..."
                  value={form.comment}
                  onChange={e => setForm(p => ({ ...p, comment: e.target.value }))} />
              </label>

              <div className={styles.orderSummary}>
                <span>{items.length} поз. на суму:</span>
                <strong>{total.toLocaleString('uk-UA')} ₴</strong>
              </div>

              <button type="submit" className={styles.orderBtn} disabled={submitting}>
                {submitting ? 'Відправка...' : '✓ Підтвердити замовлення'}
              </button>
            </form>
          </div>
        )}

        {/* SUCCESS STEP */}
        {step === 'success' && (
          <div className={styles.body}>
            <div className={styles.success}>
              <div className={styles.successIcon}>🎉</div>
              <h3>Дякуємо за замовлення!</h3>
              <p>Номер замовлення: <strong>#{orderId}</strong></p>
              <p>Ми зв'яжемося з вами найближчим часом для підтвердження.</p>
              <a href="tel:+380671234567" className={styles.phoneLink}>
                📞 +38 (067) 123-45-67
              </a>
              <button className={styles.orderBtn} onClick={onClose}>
                Закрити
              </button>
            </div>
          </div>
        )}

      </div>
    </div>
  )
}
