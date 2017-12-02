export interface Observer<T> {
    closed?: boolean;
    next: (value: T) => void;
    error: (err: any) => void;
    complete: () => void;
}
export interface Observable<T> {
    subscribe(observer: Observer<T>): void;
}
export declare class Subject<T> implements Observable<T> {
    observers: Observer<T>[];
    constructor();
    next(item: T): void;
    error(err: any): void;
    complete(): void;
    subscribe(observer: Observer<T>): void;
}
