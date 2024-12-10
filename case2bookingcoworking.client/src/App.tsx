import React, { useState } from 'react';
import { Layout, Menu, Button } from 'antd';
import MainPage from './components/MainPage';
import ProfilePage from './components/ProfilePage';
import LoginForm from './components/LoginForm';
import RegisterForm from './components/RegisterForm';
import './App.css'

const { Sider, Content } = Layout;

const App: React.FC = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [isRegistering, setIsRegistering] = useState(false); // Флаг для переключения между входом и регистрацией
  const [selectedMenu, setSelectedMenu] = useState('home');

  const handleLogout = () => {
    setIsLoggedIn(false);
    setSelectedMenu('home');
  };

  const handleLogin = () => {
    setIsLoggedIn(true);
  };

  const renderContent = () => {
    switch (selectedMenu) {
      case 'profile':
        return <ProfilePage />;
      case 'home':
        return <MainPage />;
      default:
        return <MainPage />;
    }
  };

  return (
    <Layout style={{ margin: '0', minHeight: '100vh', minWidth: '100vw', background: 'rgb(44, 44, 44)' }}>
      {!isLoggedIn ? (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
          <div style={{ maxWidth: '300px', width: '100%' }}>
            {isRegistering ? (
              <RegisterForm onRegister={() => setIsRegistering(false)} />
            ) : (
              <LoginForm onLogin={handleLogin} onSwitchToRegister={() => setIsRegistering(true)} />
            )}
          </div>
        </div>
      ) : (
        <Layout>
          <Sider
            width={200}
            style={{
              background: '#001529',
              color: 'white',
              position: 'relative',
            }}
          >
            {/* Меню навигации */}
            <Menu
              theme="dark"
              mode="inline"
              defaultSelectedKeys={['home']}
              selectedKeys={[selectedMenu]}
              onClick={(e: any) => setSelectedMenu(e.key)}
              items={[
                { key: 'home', label: 'Главная' },
                { key: 'profile', label: 'Личный кабинет' },
              ]}
            />

            {/* Кнопка "Выйти" */}
            <div
              style={{
                position: 'absolute',
                bottom: '16px',
                left: '16px',
                right: '16px',
              }}
            >
              <Button type="primary" danger block onClick={handleLogout}>
                Выйти
              </Button>
            </div>
          </Sider>
          <Layout>
            <Content style={{ margin: '16px', padding: '16px', background: '#fff' }}>
              {renderContent()}
            </Content>
          </Layout>
        </Layout>
      )}
    </Layout>
  );
};

export default App;