// Status and response body from request
export type FetchResponse<T> = {
    body: T;
    status: number;
}

// Used as body field in the FetchResponse
export type DataArrayResponse<T> = {
    data: T[];
    errors: string[];
}

export async function httpDelete(route: string, id: string): Promise<FetchResponse<null>> {
    let url = new URL(route, getOrigin());
    url.searchParams.append("id", id);
    let response = await fetch(url.toString(), {
        method: "DELETE",
        mode: 'cors',
        headers: {
            'Authorization': getBearerToken(),
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
    })
    return {body: null, status: response.status};
}


export async function httpGet(route: string): Promise<FetchResponse<null>> {
    let url = new URL(route, getOrigin());
    let response = await fetch(url.toString(), {
        method: "GET",
        mode: 'cors',
        headers: {
            'Authorization': getBearerToken(),
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
    });
    return {body: null, status: response.status};
}

export async function httpGetWithBody<T>(route: string): Promise<FetchResponse<T>> {
    let url = new URL(route, getOrigin());
    let response = await fetch(url.toString(), {
        method: "GET",
        mode: 'cors',
        headers: {
            'Authorization': getBearerToken(),
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
    });
    let body = await response.json();
    return {body: body, status: response.status};
}

export async function httpPost<T>(route: string, data: T): Promise<FetchResponse<null>> {
    let url = new URL(route, getOrigin());
    let response = await fetch(url.toString(), {
        method: "POST",
        mode: 'cors',
        headers: {
            'Authorization': getBearerToken(),
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
    return {body: null, status: response.status};
}

export async function httpPostWithBody<TIn, TOut>(route: string, data: TIn): Promise<FetchResponse<TOut>> {
    let url = new URL(route, getOrigin());
    let response = await fetch(url.toString(), {
        method: "POST",
        mode: 'cors',
        headers: {
            'Authorization': getBearerToken(),
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
    let body = await response.json();
    return {body: body, status: response.status};
}


export function getOrigin(): string {
    if (window.location.hostname === "localhost") {
        return "http://localhost:5116";
    }
    return window.location.origin;
}

export function getBearerToken(): string {
    let token = localStorage.getItem("authToken");
    return "Bearer " + token;
}

export function setToken(token: string): void {
    localStorage.setItem("authToken", token);
}