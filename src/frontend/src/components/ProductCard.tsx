import { useState } from 'react'
import type { Product } from '../api'
import { useCart } from '../context/CartContext'
import styles from './ProductCard.module.css'

interface Props {
  product: Product
}

// Унікальний градієнт для кожної породи дерева
const speciesGradients: Record<string, string> = {
  'Сосна':  'linear-gradient(135deg, #8B6914 0%, #C4923A 35%, #E8B860 60%, #A0742A 100%)',
  'Дуб':    'linear-gradient(135deg, #5C3D11 0%, #8B5E2A 35%, #C4924A 60%, #6B4720 100%)',
  'Ялина':  'linear-gradient(135deg, #2D5A27 0%, #4A8A3A 35%, #7AB86A 60%, #3A6E30 100%)',
  'Липа':   'linear-gradient(135deg, #7A6B2A 0%, #C4A84A 35%, #E8D080 60%, #9A8A3A 100%)',
}

const getGradient = (species: string) =>
  speciesGradients[species] ||
  'linear-gradient(135deg, #7A5C3A 0%, #B08050 35%, #D4A870 60%, #8A6040 100%)'

const getRings = (species: string) => {
  const colors: Record<string, string> = {
    'Сосна': 'rgba(200,150,60,0.15)',
    'Дуб':   'rgba(140,90,40,0.15)',
    'Ялина': 'rgba(80,160,80,0.15)',
    'Липа':  'rgba(200,180,60,0.15)',
  }
  return colors[species] || 'rgba(180,130,70,0.15)'
}

export default function ProductCard({ product }: Props) {
  const { addItem, items } = useCart()
  const [hovered, setHovered] = useState(false)
  const inCart = items.some(i => i.product.id === product.id)
  const price = product.unit === 'м³' ? product.pricePerCubicMeter : product.pricePerPiece
  const gradient = getGradient(product.species)
  const ringColor = getRings(product.species)

  return (
    <article
      className={`${styles.card} ${!product.inStock ? styles.outOfStockCard : ''}`}
      onMouseEnter={() => setHovered(true)}
      onMouseLeave={() => setHovered(false)}
    >
      {/* IMAGE ZONE */}
      <div className={styles.imageZone} style={{ background: gradient }}>
        {/* Деревні кільця */}
        <div className={styles.rings} style={{ '--ring-color': ringColor } as React.CSSProperties} />

        {/* Текстура волокна */}
        <div className={styles.woodFiber} />

        {/* Центральний елемент */}
        <div className={styles.woodIcon}>
          <span className={styles.woodEmoji}>🪵</span>
          <div className={styles.woodShadow} />
        </div>

        {/* Розміри поверх фото */}
        <div className={styles.dimensionsBadge}>
          <span>📐</span> {product.dimensions}
        </div>

        {/* Badges */}
        <div className={styles.badges}>
          {product.isFeatured && product.inStock && (
            <span className={styles.badgeHit}>🔥 Хіт</span>
          )}
          {!product.inStock && (
            <span className={styles.badgeOut}>Немає</span>
          )}
        </div>

        {/* Hover overlay з кнопкою */}
        <div className={`${styles.hoverOverlay} ${hovered ? styles.hoverVisible : ''}`}>
          <button
            className={`${styles.quickAdd} ${inCart ? styles.quickInCart : ''} ${!product.inStock ? styles.quickDisabled : ''}`}
            onClick={() => product.inStock && addItem(product)}
            disabled={!product.inStock}
          >
            {inCart ? '✓ В кошику' : '+ До кошика'}
          </button>
        </div>
      </div>

      {/* BODY */}
      <div className={styles.body}>
        {/* Порода + сорт */}
        <div className={styles.tags}>
          <span className={styles.tagSpecies}>{product.species}</span>
          {product.grade && product.grade !== '—' && (
            <span className={styles.tagGrade}>{product.grade}</span>
          )}
          <span className={styles.tagCategory}>{product.categoryName}</span>
        </div>

        <h3 className={styles.name}>{product.name}</h3>
        <p className={styles.desc}>{product.description}</p>

        {/* Ціна */}
        <div className={styles.priceRow}>
          <div className={styles.priceMain}>
            <span className={styles.priceValue}>
              {price.toLocaleString('uk-UA')}
            </span>
            <span className={styles.priceCurrency}>₴</span>
            <span className={styles.priceUnit}>/ {product.unit}</span>
          </div>

          {/* Якщо є обидві ціни — показати обидві */}
          {product.unit === 'м³' && product.pricePerPiece > 0 && (
            <span className={styles.priceAlt}>
              {product.pricePerPiece.toLocaleString('uk-UA')} ₴/шт
            </span>
          )}
        </div>

        {/* Кнопка (для десктопу прихована при ховері, видима на мобілі) */}
        <button
          className={`${styles.addBtn} ${inCart ? styles.inCart : ''} ${!product.inStock ? styles.disabled : ''}`}
          onClick={() => product.inStock && addItem(product)}
          disabled={!product.inStock}
        >
          {!product.inStock
            ? 'Немає в наявності'
            : inCart
              ? '✓ Вже в кошику'
              : '+ Додати до кошика'}
        </button>
      </div>
    </article>
  )
}
