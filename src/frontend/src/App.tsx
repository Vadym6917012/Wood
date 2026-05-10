import { useState } from 'react'
import { CartProvider } from './context/CartContext'
import Navbar from './components/Navbar'
import CartModal from './components/CartModal'
import HomePage from './pages/HomePage'

export default function App() {
  const [cartOpen, setCartOpen] = useState(false)

  return (
    <CartProvider>
      <Navbar onCartClick={() => setCartOpen(true)} />
      <CartModal open={cartOpen} onClose={() => setCartOpen(false)} />
      <HomePage />
    </CartProvider>
  )
}
