import { BaseBearerTokenAuthenticationProvider } from "@microsoft/kiota-abstractions";
import { FetchRequestAdapter } from "@microsoft/kiota-http-fetchlibrary";
import { BikeServiceClient, createBikeServiceClient } from "../../gen/bikeServiceClient.ts";
import { MyTokenProvider } from "../Models/TokenProvider.ts";

export function getClient() : BikeServiceClient {

// API requires no authentication, so use the anonymous
// authentication provider
    const accessTokenProvider = new MyTokenProvider();
    const authProvider = new BaseBearerTokenAuthenticationProvider(accessTokenProvider);
// Create request adapter using the fetch-based implementation
    const adapter = new FetchRequestAdapter(authProvider);
// Create the API client
    const client = createBikeServiceClient(adapter);
    return client;
}
