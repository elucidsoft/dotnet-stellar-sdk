export declare class HttpError extends Error {
    statusCode: number;
    constructor(errorMessage: string, statusCode: number);
}
