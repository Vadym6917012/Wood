import { useEffect, useState } from 'react'
import { productsApi, type Product, type Category } from '../api'
import ProductCard from '../components/ProductCard'
import styles from './HomePage.module.css'

export default function HomePage() {
  const [products, setProducts] = useState<Product[]>([])
  const [categories, setCategories] = useState<Category[]>([])
  const [activeCat, setActiveCat] = useState<number | null>(null)
  const [search, setSearch] = useState('')
  const [sortBy, setSortBy] = useState('')
  const [loading, setLoading] = useState(true)
  const [formSent, setFormSent] = useState(false)
  const [contactForm, setContactForm] = useState({ name: '', phone: '', message: '' })

  useEffect(() => {
    productsApi.getCategories().then(setCategories)
  }, [])

  useEffect(() => {
    setLoading(true)
    productsApi.getAll({
      categoryId: activeCat ?? undefined,
      search: search || undefined,
      sortBy: sortBy || undefined,
    })
      .then(setProducts)
      .finally(() => setLoading(false))
  }, [activeCat, search, sortBy])

  const scrollTo = (id: string) =>
    document.getElementById(id)?.scrollIntoView({ behavior: 'smooth' })

  const handleContactSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    setFormSent(true)
  }

  return (
    <main>

      {/* HERO */}
      <section className={styles.hero} id="hero">
        <div className={styles.heroBg} />
        <div className={styles.heroGrain} />
        <div className={styles.heroRings} />
        <div className={styles.heroContent}>
          <div className={styles.heroText}>
            <div className={styles.heroLabel}>Рівне • Доставка по місту</div>
            <h1 className={styles.heroTitle}>
              Якісні<br />
              <span className={styles.heroAccent}>пиломатеріали</span><br />
              для вашого дому
            </h1>
            <p className={styles.heroSub}>
              Дошки, брус, вагонка. Широкий асортимент,
              доступні ціни, швидка доставка по Рівному.
            </p>
            <div className={styles.heroBtns}>
              <button className={styles.btnPrimary} onClick={() => scrollTo('catalog')}>
                Переглянути каталог →
              </button>
              <button className={styles.btnOutline} onClick={() => scrollTo('contact')}>
                Зв'язатися з нами
              </button>
            </div>
            <div className={styles.heroStats}>
              <div className={styles.stat}>
                <span className={styles.statNum}>12+</span>
                <span className={styles.statLabel}>років роботи</span>
              </div>
              <div className={styles.statDivider} />
              <div className={styles.stat}>
                <span className={styles.statNum}>500+</span>
                <span className={styles.statLabel}>клієнтів</span>
              </div>
              <div className={styles.statDivider} />
              <div className={styles.stat}>
                <span className={styles.statNum}>50+</span>
                <span className={styles.statLabel}>позицій товару</span>
              </div>
            </div>
          </div>
          <div className={styles.heroVisual}>
            <div className={styles.woodStack}>
              {[0,1,2,3,4].map(i => (
                <div key={i} className={styles.plank}
                  style={{ '--rot': `${(i%3-1)*1.5}deg`, '--off': `${i*-6}px` } as React.CSSProperties} />
              ))}
            </div>
            <div className={styles.heroEmoji}>🪵</div>
          </div>
        </div>
      </section>

      {/* ABOUT / FEATURES */}
      <section className={styles.section} id="about">
        <div className={styles.container}>
          <div className={styles.sectionHead}>
            <span className={styles.sectionLabel}>Переваги</span>
            <h2 className={styles.sectionTitle}>Чому обирають нас</h2>
          </div>
          <div className={styles.featuresGrid}>
            {[
              { icon: '🌲', title: 'Власне виробництво', desc: 'Матеріал напряму від виробника без посередників.' },
              { icon: '📐', title: 'Точні розміри', desc: 'Нарізка за вашими замірами. Відхилення не більше 1мм.' },
              { icon: '🚚', title: 'Доставка по Рівному', desc: 'Власний транспорт. Доставка по місту та Рівненській області.' },
              { icon: '⚡', title: 'Швидко', desc: 'Замовлення оброблюємо в день звернення. Доставка 1-2 дні.' },
              { icon: '💰', title: 'Чесні ціни', desc: 'Без прихованих доплат. Ціна з доставкою погоджується одразу.' },
              { icon: '🏆', title: 'Перевірена якість', desc: 'Сировина від місцевих лісозаготівельників з Рівненщини.' },
            ].map(f => (
              <div key={f.title} className={styles.featureCard}>
                <span className={styles.featureIcon}>{f.icon}</span>
                <h3 className={styles.featureTitle}>{f.title}</h3>
                <p className={styles.featureDesc}>{f.desc}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* CATALOG */}
      <section className={styles.catalogSection} id="catalog">
        <div className={styles.container}>
          <div className={styles.sectionHead}>
            <span className={styles.sectionLabel}>Асортимент</span>
            <h2 className={styles.sectionTitle}>Каталог товарів</h2>
          </div>

          <div className={styles.filtersBar}>
            <div className={styles.catTabs}>
              <button
                className={`${styles.catTab} ${activeCat === null ? styles.catTabActive : ''}`}
                onClick={() => setActiveCat(null)}
              >
                🏪 Всі
                <span className={styles.catTabCount}>{products.length}</span>
              </button>
              {categories.map(c => (
                <button
                  key={c.id}
                  className={`${styles.catTab} ${activeCat === c.id ? styles.catTabActive : ''}`}
                  onClick={() => setActiveCat(c.id)}
                >
                  {c.icon} {c.name}
                  <span className={styles.catTabCount}>{c.productCount}</span>
                </button>
              ))}
            </div>
            <div className={styles.filterControls}>
              <div className={styles.searchWrap}>
                <span className={styles.searchIcon}>🔍</span>
                <input
                  type="text"
                  placeholder="Пошук за назвою або породою..."
                  className={styles.searchInput}
                  value={search}
                  onChange={e => setSearch(e.target.value)}
                />
                {search && (
                  <button
                    onClick={() => setSearch('')}
                    style={{ background: 'none', border: 'none', cursor: 'pointer', color: 'var(--gray-400)', fontSize: 18, lineHeight: 1, padding: '0 4px' }}
                  >×</button>
                )}
              </div>
              <select
                className={styles.sortSelect}
                value={sortBy}
                onChange={e => setSortBy(e.target.value)}
              >
                <option value="">За замовчуванням</option>
                <option value="price_asc">Ціна: від низької</option>
                <option value="price_desc">Ціна: від високої</option>
                <option value="name">Назва А → Я</option>
              </select>
            </div>
          </div>

          <div className={styles.resultsInfo}>
            <strong>{products.length}</strong>
            <span>товарів знайдено</span>
            {(search || activeCat) && (
              <>
                <div className={styles.resultsDivider} />
                <button
                  onClick={() => { setSearch(''); setActiveCat(null) }}
                  style={{ background: 'none', border: 'none', cursor: 'pointer', color: 'var(--amber)', fontSize: 13, fontWeight: 600, padding: 0, fontFamily: 'var(--font-body)' }}
                >
                  Скинути фільтри ×
                </button>
              </>
            )}
          </div>

          {loading ? (
            <div className={styles.loading}>
              <div className={styles.spinner} />
              <span className={styles.loadingText}>Завантаження товарів...</span>
            </div>
          ) : products.length === 0 ? (
            <div className={styles.empty}>
              <span className={styles.emptyEmoji}>🪵</span>
              <p>Товарів не знайдено</p>
              <span>Спробуйте інший запит або категорію</span>
              <button onClick={() => { setSearch(''); setActiveCat(null) }} className={styles.resetBtn}>
                Показати всі товари
              </button>
            </div>
          ) : (
            <div className={styles.productsGrid}>
              {products.map(p => <ProductCard key={p.id} product={p} />)}
            </div>
          )}
        </div>
      </section>

      {/* DELIVERY */}
      <section className={styles.deliverySection} id="delivery">
        <div className={styles.container}>
          <div className={styles.deliveryInner}>
            <div className={styles.deliveryText}>
              <span className={styles.sectionLabelLight}>Логістика</span>
              <h2 className={styles.sectionTitleLight}>Доставка по Рівному</h2>
              <p className={styles.deliveryDesc}>
                Здійснюємо доставку пиломатеріалів власним вантажним транспортом
                по всьому місту та Рівненській області.
              </p>
              <ul className={styles.deliveryList}>
                <li>✅ Доставка в межах міста — від 700 грн</li>
                <li>✅ По Рівненській області — за домовленістю</li>
                <li>✅ Підйом на поверх — обговорюється окремо</li>
                <li>✅ Вивантаження вантажниками — можливо</li>
              </ul>
              <a href="tel:+380671234567" className={styles.btnPrimaryLight}>
                📞 Уточнити вартість
              </a>
            </div>
            <div className={styles.deliveryCards}>
              {[
                { icon: '🚚', title: 'Доставка по Рівному', sub: ' щодня 8:00 — 19:00' },
                { icon: '💳', title: 'Оплата', sub: ' готівка або картка' },
              ].map(c => (
                <div key={c.title} className={styles.deliveryCard}>
                  <span className={styles.deliveryEmoji}>{c.icon}</span>
                  <div>
                    <strong>{c.title }</strong>
                    <span>{c.sub}</span>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </div>
      </section>

      {/* CONTACT */}
      <section className={styles.section} id="contact">
        <div className={styles.container}>
          <div className={styles.contactGrid}>
            <div>
              <span className={styles.sectionLabel}>Зв'язок</span>
              <h2 className={styles.sectionTitle}>Контакти</h2>
              <div className={styles.contactItems}>
                <a href="tel:+380682418001" className={styles.contactItem}>
                  <span>📞</span><div><strong>Телефон</strong><span>+38 (068) 241-80-01</span></div>
                </a>
                <a href="tel:+380682418001" className={styles.contactItem}>
                  <span>📱</span><div><strong>Viber / Telegram</strong><span>+38 (068) 241-80-01</span></div>
                </a>
                <div className={styles.contactItem}>
                  <span>🕐</span><div><strong>Графік роботи</strong><span>Пн-Сб 8:00–19:00</span></div>
                </div>
              </div>
            </div>
            <div className={styles.contactFormWrap}>
              <h3 className={styles.formTitle}>Залишити заявку</h3>
              {formSent ? (
                <div className={styles.formSuccess}>
                  ✅ Дякуємо! Ми зв'яжемося з вами найближчим часом.
                </div>
              ) : (
                <form className={styles.form} onSubmit={handleContactSubmit}>
                  <input className={styles.input} placeholder="Ваше ім'я *" required
                    value={contactForm.name}
                    onChange={e => setContactForm(p => ({ ...p, name: e.target.value }))} />
                  <input className={styles.input} type="tel" placeholder="Телефон *" required
                    value={contactForm.phone}
                    onChange={e => setContactForm(p => ({ ...p, phone: e.target.value }))} />
                  <textarea className={`${styles.input} ${styles.textarea}`} rows={4}
                    placeholder="Що вас цікавить?"
                    value={contactForm.message}
                    onChange={e => setContactForm(p => ({ ...p, message: e.target.value }))} />
                  <button type="submit" className={styles.btnPrimary}>
                    Надіслати заявку
                  </button>
                </form>
              )}
            </div>
          </div>
        </div>
      </section>

      {/* FOOTER */}
      <footer className={styles.footer}>
        <div className={styles.container}>
          <div className={styles.footerInner}>
            <div className={styles.footerLogo}>🪵 <strong>ЛісоПром Рівне</strong></div>
            <p>Якісні пиломатеріали з доставкою по Рівному</p>
            <p className={styles.footerCopy}>© 2024 ЛісоПром Рівне</p>
          </div>
        </div>
      </footer>

    </main>
  )
}
