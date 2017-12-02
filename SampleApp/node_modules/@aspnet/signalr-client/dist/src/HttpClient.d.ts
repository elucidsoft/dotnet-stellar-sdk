export interface IHttpClient {
    get(url: string, headers?: Map<string, string>): Promise<string>;
    options(url: string, headers?: Map<string, string>): Promise<string>;
    post(url: string, content: string, headers?: Map<string, string>): Promise<string>;
}
export declare class HttpClient implements IHttpClient {
    get(url: string, headers?: Map<string, string>): Promise<string>;
    options(url: string, headers?: Map<string, string>): Promise<string>;
    post(url: string, content: string, headers?: Map<string, string>): Promise<string>;
    private xhr(method, url, headers?, content?);
}
