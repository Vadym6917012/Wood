import { type Product } from '../api'
import { useCart } from '../context/CartContext'
import styles from './ProductCard.module.css'

interface Props { product: Product }

export default function ProductCard({ product }: Props) {
  const { addItem, items } = useCart()
  const inCart = items.some(i => i.product.id === product.id)
  const price = product.unit === 'м³' ? product.pricePerCubicMeter : product.pricePerPiece

  return (
    <article className={styles.card}>
      <div className={styles.imageWrap}>
        <div className={styles.imagePlaceholder}>
          <span className={styles.woodIcon}>🪵</span>
          <div className={styles.grain} />
        </div>
        {!product.inStock && (
          <div className={styles.outOfStock}>Немає в наявності</div>
        )}
        {product.isFeatured && product.inStock && (
          <div className={styles.featured}>Хіт</div>
        )}
      </div>

      <div className={styles.body}>
        <div className={styles.meta}>
          <span className={styles.species}>{product.species}</span>
          <span className={styles.grade}>{product.grade}</span>
        </div>
        <h3 className={styles.name}>{product.name}</h3>
        <p className={styles.dimensions}>📐 {product.dimensions}</p>
        <p className={styles.desc}>{product.description}</p>
        <div className={styles.footer}>
          <div className={styles.priceBlock}>
            <span className={styles.price}>{price.toLocaleString('uk-UA')} ₴</span>
            <span className={styles.unit}>/ {product.unit}</span>
          </div>
          <button
            className={`${styles.addBtn} ${inCart ? styles.inCart : ''} ${!product.inStock ? styles.disabled : ''}`}
            onClick={() => product.inStock && addItem(product)}
            disabled={!product.inStock}
          >
            {inCart ? '✓ В кошику' : '+ До кошика'}
          </button>
        </div>
      </div>
    </article>
  )
}