import { useState, useEffect } from 'react'
import { useCart } from '../context/CartContext'
import styles from './Navbar.module.css'

interface Props {
  onCartClick: () => void
}

export default function Navbar({ onCartClick }: Props) {
  const { count } = useCart()
  const [scrolled, setScrolled] = useState(false)
  const [menuOpen, setMenuOpen] = useState(false)

  useEffect(() => {
    const handler = () => setScrolled(window.scrollY > 40)
    window.addEventListener('scroll', handler)
    return () => window.removeEventListener('scroll', handler)
  }, [])

  const scrollTo = (id: string) => {
    setMenuOpen(false)
    document.getElementById(id)?.scrollIntoView({ behavior: 'smooth' })
  }

  return (
    <header className={`${styles.nav} ${scrolled ? styles.scrolled : ''}`}>
      <div className={styles.inner}>
        <button className={styles.logo} onClick={() => scrollTo('hero')}>
          <span className={styles.logoIcon}>🪵</span>
          <div>
            <span className={styles.logoMain}>ЛісоПром</span>
            <span className={styles.logoSub}>Рівне</span>
          </div>
        </button>

        <nav className={`${styles.links} ${menuOpen ? styles.open : ''}`}>
          <button onClick={() => scrollTo('about')}>Про нас</button>
          <button onClick={() => scrollTo('catalog')}>Каталог</button>
          <button onClick={() => scrollTo('delivery')}>Доставка</button>
          <button onClick={() => scrollTo('contact')}>Контакти</button>
        </nav>

        <div className={styles.actions}>
          <a href="tel:+380682418001" className={styles.phone}>
            📞 +38 (068) 241-80-01
          </a>
          <button className={styles.cartBtn} onClick={onCartClick}>
            🛒
            {count > 0 && <span className={styles.badge}>{count}</span>}
          </button>
          <button className={styles.burger} onClick={() => setMenuOpen(!menuOpen)}>
            <span /><span /><span />
          </button>
        </div>
      </div>
    </header>
  )
}