export declare type CreateDevServerResult = {
    Port: number;
    PublicPaths: string[];
};
export interface CreateDevServerCallback {
    (error: any, result: CreateDevServerResult): void;
}
export declare function createWebpackDevServer(callback: CreateDevServerCallback, optionsJson: string): void;
