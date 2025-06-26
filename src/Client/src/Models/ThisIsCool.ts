import { AnonymousAuthenticationProvider } from "@microsoft/kiota-abstractions";
import { FetchRequestAdapter } from "@microsoft/kiota-http-fetchlibrary";
import { createBikeServiceClient } from "../../gen/bikeServiceClient.ts";

export async function testing() {
// API requires no authentication, so use the anonymous
// authentication provider
    const authProvider = new AnonymousAuthenticationProvider();
// Create request adapter using the fetch-based implementation
    const adapter = new FetchRequestAdapter(authProvider);
// Create the API client
    const client = createBikeServiceClient(adapter);
    const response = await client.api.login.post({email: "email.com"})
    console.log(response)
}
