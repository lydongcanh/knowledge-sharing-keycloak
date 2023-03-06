public class WebhookEventListenerProviderFactory implements EventListenerProviderFactory {

    private static final String LISTENER_ID = "event-listener-extension";

    @Override
    public EventListenerProvider create(KeycloakSession session) {
        return new WebhookEventListenerProvider();
    }

    @Override
    public void init(Config.Scope scope) {

    }

    @Override
    public void postInit(KeycloakSessionFactory keycloakSessionFactory) {

    }

    @Override
    public void close() {

    }

    @Override
    public String getId() {
        return LISTENER_ID;
    }

}