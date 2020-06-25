import * as PointerDatas from './PointerDatas';

// The top-level state object
export interface ApplicationState {
    pointerDatas: PointerDatas.PointerDatasState;
}

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
