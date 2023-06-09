export interface EmptyResult {
    isSuccessful: boolean,
    clientErrorMessage: string
}

export interface Result<TBody> extends EmptyResult {
    value : TBody
}