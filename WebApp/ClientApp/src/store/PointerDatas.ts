import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

export interface PointerData {
    name: string;
    description: string;
    north: number;
    east: number;
    hight: number;
    value: number;
}
export interface PointerDatasState {
    isLoading: boolean;
    datas: PointerData[];
}

interface RequestPointerDatasAction {
    type: 'REQUEST_POINTER_DATAS';
}

interface ReceivePointerDatasAction {
    type: 'RECEIVE_POINTER_DATAS';
    datas: PointerData[];
}

export type KnownAction = RequestPointerDatasAction | ReceivePointerDatasAction;

export const actionCreators = {
    requestPointerDatas: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.pointerDatas) {
            fetch(`pointerdata`)
                .then(response => response.json() as Promise<PointerData[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_POINTER_DATAS', datas: data });
                });

            dispatch({ type: 'REQUEST_POINTER_DATAS' });
        }
    }
};

const unloadedState: PointerDatasState = { datas: [], isLoading: false };

export const reducer: Reducer<PointerDatasState> = (state: PointerDatasState | undefined, action: KnownAction): PointerDatasState => {
    if (state === undefined) {
        return unloadedState;
    }

    switch (action.type) {
        case 'REQUEST_POINTER_DATAS':
            return {
                datas: state.datas,
                isLoading: true
            };
        case 'RECEIVE_POINTER_DATAS':
            return {
                datas: action.datas,
                isLoading: false
            };
    }

    // return state;
};