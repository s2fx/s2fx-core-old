
export interface IS2HttpClient {
    baseUri: string

    getAsJson<TResponseData=any>(path: string): Promise<TResponseData> //有返回值无参数
    getAsJson<TResponseData=any>(path: string, params: {[key: string]: any}): Promise<TResponseData> //有返回值有参数
    getAsJson<TResponseData=any>(path: string, params?: {[key: string]: any}): Promise<TResponseData>

    postAsJson<TResponseData>(path: string): Promise<TResponseData> //有返回值无参数
    postAsJson<TResponseData>(path: string, params:  {[key: string]: any}): Promise<TResponseData> //有返回值有参数
    postAsJson<TResponseData>(path: string, params?: {[key: string]: any}): Promise<TResponseData>

    putAsJson<TResponseData>(path: string): Promise<TResponseData> //有返回值无参数
    putAsJson<TResponseData>(path: string, params:  {[key: string]: any}): Promise<TResponseData> //有返回值有参数
    putAsJson<TResponseData>(path: string, params?: {[key: string]: any}): Promise<TResponseData>

    delete(path: string, params: {[key: string]: any}): Promise<void>
    delete(path: string, params?: {[key: string]: any}): Promise<void>
}

