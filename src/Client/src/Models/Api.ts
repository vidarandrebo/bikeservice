import {
    Configuration,
    type ConfigurationParameters,
    type HTTPHeaders,
    BikeApi,
    RefreshApi,
    PartApi,
    TypeApi,
    RegisterApi,
    LoginApi
} from "../Gen";
import { loadBearerTokenFromLocalStorage } from "./Auth/User.ts";

class CfgParams implements ConfigurationParameters {
    basePath?: string | undefined;

    get headers(): HTTPHeaders | undefined {
        const bearerToken = loadBearerTokenFromLocalStorage();
        return {
            Authorization: `Bearer ${bearerToken}`
        };
    }

    constructor() {
        this.basePath = "";
    }
}

export function getBikeApi(): BikeApi {
    const cfg = new Configuration(new CfgParams());
    const api = new BikeApi(cfg);
    return api;
}
export function getPartApi(): PartApi {
    const cfg = new Configuration(new CfgParams());
    const api = new PartApi(cfg);
    return api;
}
export function getLoginApi(): LoginApi {
    const cfg = new Configuration(new CfgParams());
    const api = new LoginApi(cfg);
    return api;
}
export function getRegisterApi(): RegisterApi {
    const cfg = new Configuration(new CfgParams());
    const api = new RegisterApi(cfg);
    return api;
}
export function getRefreshApi(): RefreshApi {
    const cfg = new Configuration(new CfgParams());
    const api = new RefreshApi(cfg);
    return api;
}
export function getTypeApi(): TypeApi {
    const cfg = new Configuration(new CfgParams());
    const api = new TypeApi(cfg);
    return api;
}
