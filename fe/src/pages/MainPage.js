import React, { useState, useEffect } from "react";
import keycloak from "../helpers/Keycloak";

export default function MainPage() {

    return <iframe
        src='https://localhost:7223/hangfire'
        frameBorder="0"
        allowFullScreen
        style={{
            height: '100vh',
            width: '100vw',
            boxSizing: 'border-box',
        }}
    />;
};