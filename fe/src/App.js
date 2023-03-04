import React from "react";
import { ChakraProvider } from '@chakra-ui/react'
import { BrowserRouter, Route, Routes } from "react-router-dom";
import WelcomePage from "./pages/Homepage";
import SecuredPage from "./pages/Securedpage";
import Nav from "./components/Nav";

export default function App() {
    return (
        <ChakraProvider>
            <Nav />
            <BrowserRouter>
                <Routes>
                    <Route exact path="/" element={<WelcomePage />} />
                    <Route path="/secured" element={<SecuredPage />} />
                </Routes>
            </BrowserRouter>
        </ChakraProvider>
    );
}