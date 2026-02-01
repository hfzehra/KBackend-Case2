import React, { useState, useEffect } from 'react';
import { apiService, Product, CreateProductRequest } from './services/api';
import './App.css';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(apiService.isAuthenticated());
  const [products, setProducts] = useState<Product[]>([]);
  const [showLogin, setShowLogin] = useState(true);
  const [error, setError] = useState('');
  
  // Auth form
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [fullName, setFullName] = useState('');
  
  // Product form
  const [productName, setProductName] = useState('');
  const [productDesc, setProductDesc] = useState('');
  const [productPrice, setProductPrice] = useState('');
  const [productStock, setProductStock] = useState('');
  const [editingId, setEditingId] = useState<string | null>(null);

  useEffect(() => {
    if (isLoggedIn) {
      loadProducts();
    }
  }, [isLoggedIn]);

  const loadProducts = async () => {
    try {
      const data = await apiService.getProducts();
      setProducts(data);
    } catch (err) {
      setError('Ürünler yüklenemedi');
    }
  };

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await apiService.login({ email, password });
      setIsLoggedIn(true);
      setError('');
    } catch (err) {
      setError('Giriş başarısız');
    }
  };

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await apiService.register({ email, password, fullName });
      setIsLoggedIn(true);
      setError('');
    } catch (err) {
      setError('Kayıt başarısız');
    }
  };

  const handleLogout = () => {
    apiService.clearToken();
    setIsLoggedIn(false);
    setProducts([]);
  };

  const handleProductSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const productData: CreateProductRequest = {
      name: productName,
      description: productDesc,
      price: parseFloat(productPrice),
      stock: parseInt(productStock),
    };
    
    try {
      if (editingId) {
        await apiService.updateProduct(editingId, productData);
      } else {
        await apiService.createProduct(productData);
      }
      loadProducts();
      clearProductForm();
    } catch (err) {
      setError('İşlem başarısız');
    }
  };

  const handleEdit = (product: Product) => {
    setEditingId(product.id);
    setProductName(product.name);
    setProductDesc(product.description);
    setProductPrice(product.price.toString());
    setProductStock(product.stock.toString());
  };

  const handleDelete = async (id: string) => {
    try {
      await apiService.deleteProduct(id);
      loadProducts();
    } catch (err) {
      setError('Silme başarısız');
    }
  };

  const clearProductForm = () => {
    setEditingId(null);
    setProductName('');
    setProductDesc('');
    setProductPrice('');
    setProductStock('');
  };

  if (!isLoggedIn) {
    return (
      <div className="container">
        <h1>Product Management</h1>
        {error && <div className="error">{error}</div>}
        
        <div className="tabs">
          <button className={showLogin ? 'active' : ''} onClick={() => setShowLogin(true)}>Giriş</button>
          <button className={!showLogin ? 'active' : ''} onClick={() => setShowLogin(false)}>Kayıt</button>
        </div>

        {showLogin ? (
          <form onSubmit={handleLogin}>
            <input type="email" placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} required />
            <input type="password" placeholder="Şifre" value={password} onChange={e => setPassword(e.target.value)} required />
            <button type="submit">Giriş Yap</button>
          </form>
        ) : (
          <form onSubmit={handleRegister}>
            <input type="text" placeholder="Ad Soyad" value={fullName} onChange={e => setFullName(e.target.value)} required />
            <input type="email" placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} required />
            <input type="password" placeholder="Şifre" value={password} onChange={e => setPassword(e.target.value)} required />
            <button type="submit">Kayıt Ol</button>
          </form>
        )}
      </div>
    );
  }

  return (
    <div className="container">
      <div className="header">
        <h1>Ürün Yönetimi</h1>
        <button onClick={handleLogout}>Çıkış</button>
      </div>
      
      {error && <div className="error">{error}</div>}

      <form onSubmit={handleProductSubmit} className="product-form">
        <h3>{editingId ? 'Ürün Düzenle' : 'Yeni Ürün Ekle'}</h3>
        <input type="text" placeholder="Ürün Adı" value={productName} onChange={e => setProductName(e.target.value)} required />
        <input type="text" placeholder="Açıklama" value={productDesc} onChange={e => setProductDesc(e.target.value)} />
        <input type="number" placeholder="Fiyat" value={productPrice} onChange={e => setProductPrice(e.target.value)} required />
        <input type="number" placeholder="Stok" value={productStock} onChange={e => setProductStock(e.target.value)} required />
        <div>
          <button type="submit">{editingId ? 'Güncelle' : 'Ekle'}</button>
          {editingId && <button type="button" onClick={clearProductForm}>İptal</button>}
        </div>
      </form>

      <div className="products">
        <h3>Ürünler</h3>
        {products.length === 0 ? (
          <p>Henüz ürün yok</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>Ad</th>
                <th>Açıklama</th>
                <th>Fiyat</th>
                <th>Stok</th>
                <th>İşlemler</th>
              </tr>
            </thead>
            <tbody>
              {products.map(product => (
                <tr key={product.id}>
                  <td>{product.name}</td>
                  <td>{product.description}</td>
                  <td>{product.price} ₺</td>
                  <td>{product.stock}</td>
                  <td>
                    <button onClick={() => handleEdit(product)}>Düzenle</button>
                    <button onClick={() => handleDelete(product.id)}>Sil</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}

export default App;
