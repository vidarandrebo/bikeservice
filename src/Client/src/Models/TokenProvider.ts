import { AccessTokenProvider, AllowedHostsValidator } from "@microsoft/kiota-abstractions";
import { loadBearerTokenFromLocalStorage } from "./Auth/User.ts";

export class MyTokenProvider implements AccessTokenProvider {
    getAllowedHostsValidator(): AllowedHostsValidator {
        const validator = new AllowedHostsValidator(new Set<string>("test"));
        return validator;
    }

    getAuthorizationToken(url: string | undefined, additionalAuthenticationContext: Record<string, unknown> | undefined): Promise<string> {
        const token = loadBearerTokenFromLocalStorage();

        return Promise.resolve(token ?? "");
    }

}
