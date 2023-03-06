import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.ContentType;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.HttpClientBuilder;

@Slf4j
@NoArgsConstructor
public class WebhookEventListenerProvider implements EventListenerProvider {

    @Override
    public void onEvent(Event event) {
      SendWebhookEvent(EventUtils.toString(event));
    }

    @Override
    public void onEvent(AdminEvent adminEvent, boolean b) {
      SendWebhookEvent(EventUtils.toString(adminEvent));
    }

    @Override
    public void close() {

    }

    private void SendWebhookEvent(String event) {
      StringEntity entity = new StringEntity(event, ContentType.APPLICATION_FORM_URLENCODED);

      HttpClient httpClient = HttpClientBuilder.create().build();
      HttpPost request = new HttpPost("https://webhook.site/c0df1315-1abe-4e99-92fe-dd38c369677e");
      request.setEntity(entity);

      HttpResponse response = httpClient.execute(request);
    }
}