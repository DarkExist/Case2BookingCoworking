import React, { useEffect, useState } from 'react';
import { Layout, Menu, Button } from 'antd';
import { BrowserRouter as Router, Routes, Route, Navigate, Link } from 'react-router-dom';
import MainPage from './components/MainPage';
import ProfilePage from './components/ProfilePage';
import LoginForm from './components/LoginForm';
import RegisterForm from './components/RegisterForm';
import './App.css'

const { Sider, Content } = Layout;

const App: React.FC = () => {
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(() => {
        // Загружаем состояние из localStorage при загрузке приложения
        return localStorage.getItem('isLoggedIn') === 'true';
    });

    useEffect(() => {
        // Сохраняем состояние в localStorage при его изменении
        localStorage.setItem('isLoggedIn', isLoggedIn.toString());
    }, [isLoggedIn]);

    const handleLogout = () => {
        setIsLoggedIn(false);
    };

    const handleLogin = () => {
        setIsLoggedIn(true);
    };

    return (
        <Router>
            <Layout style={{ margin: '0', minHeight: '100vh', minWidth: '100vw', background: 'rgb(44, 44, 44)' }}>
                {!isLoggedIn ? (
                    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
                        <Routes>
                            <Route
                                path="/login"
                                element={<LoginForm onLogin={handleLogin} onSwitchToRegister={() => window.location.href = '/register'} />}
                            />
                            <Route
                                path="/register"
                                element={<RegisterForm onRegister={() => window.location.href = '/login'} />}
                            />
                            <Route path="*" element={<Navigate to="/login" replace />} />
                        </Routes>
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
                                defaultSelectedKeys={['/']}
                            >
                                <Menu.Item key="home">
                                    <Link to="/">Главная</Link>
                                </Menu.Item>
                                <Menu.Item key="profile">
                                    <Link to="/profile">Личный кабинет</Link>
                                </Menu.Item>
                            </Menu>

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
                                <Routes>
                                    <Route path="/" element={<MainPage />} />
                                    <Route path="/profile" element={<ProfilePage />} />
                                    <Route path="*" element={<Navigate to="/" replace />} />
                                </Routes>
                            </Content>
                        </Layout>
                    </Layout>
                )}
            </Layout>
        </Router>
    );
};

export default App;