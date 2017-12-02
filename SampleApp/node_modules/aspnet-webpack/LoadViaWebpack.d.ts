import 'es6-promise';
export interface LoadViaWebpackCallback<T> {
    (error: any, result: T): void;
}
export declare function loadViaWebpack<T>(webpackConfigPath: string, modulePath: string, callback: LoadViaWebpackCallback<T>): void;
