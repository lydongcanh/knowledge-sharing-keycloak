import React, { useState, useEffect } from "react";
import Keycloak from "keycloak-js";

export default function SecuredPage() {

    console.log("rendering SecuredPage...");

    const [keycloak, setKeycloak] = useState(null);
    const [authenticated, setAuthenticated] = useState(false);

    useEffect(() => {
        const keycloak = new Keycloak("./keycloak.json");
        keycloak
            .init({
                onLoad: "login-required",
            })
            .then(authenticated => {
                console.log({ keycloak });
                console.log({ authenticated });
                setKeycloak(keycloak)
                setAuthenticated(authenticated)
            }) 
            .catch(e => {
                console.error(e);
            })
    }, []);

    if (!authenticated) {
        return <h1>Not Authenticated</h1>;
    }

    return <a title="User profile" href="http://localhost:8080/realms/dev/account/#/personal-info">User profile</a>;
};