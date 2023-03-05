import React, { useEffect, useState } from 'react';
import { Center, ChakraProvider, Spinner } from '@chakra-ui/react'
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import AccessDeniedPage from './pages/AccessDeniedPage';
import MainPage from './pages/MainPage';
import WelcomePage from './pages/WelcomePage';
import Nav from './components/Nav';
import PrivateRoute from './helpers/PrivateRoute';
import keycloak from './helpers/Keycloak';

export default function App() {

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        keycloak.onAuthSuccess = () => {
            console.log("onAuthSuccess");
            setIsAuthenticated(true);
        }

        keycloak.init({
            onLoad: 'check-sso',
            checkLoginIframe: false
        }).then(authenticated => {
            setIsAuthenticated(authenticated);
        }).catch(e => {
            setIsAuthenticated(false);
            console.error(e);
        }).finally(() => {
            setIsLoading(false);
        });
    }, []);

    return (
        <ChakraProvider>
            {isLoading
                ? <Center style={{ paddingTop: '50%' }}><Spinner size='xl' /></Center>
                : <>
                    <Nav isAuthenticated={isAuthenticated} />
                    <BrowserRouter>
                        <Routes>
                            <Route exact path='/' element={<WelcomePage />} />
                            <Route path='/403' element={<AccessDeniedPage />} />
                            <Route path='/main' element={
                                <PrivateRoute isAuthenticated={isAuthenticated} >
                                    <MainPage />
                                </PrivateRoute>
                            } />
                        </Routes>
                    </BrowserRouter>
                </>
            }
        </ChakraProvider >
    );
}