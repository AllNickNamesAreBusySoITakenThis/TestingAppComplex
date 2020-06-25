"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.actionCreators = {
    requestPointerDatas: function () { return function (dispatch, getState) {
        // Only load data if it's something we don't already have (and are not already loading)
        var appState = getState();
        if (appState && appState.pointerDatas) {
            fetch("pointerdata")
                .then(function (response) { return response.json(); })
                .then(function (data) {
                dispatch({ type: 'RECEIVE_POINTER_DATAS', datas: data });
            });
            dispatch({ type: 'REQUEST_POINTER_DATAS' });
        }
    }; }
};
var unloadedState = { datas: [], isLoading: false };
exports.reducer = function (state, action) {
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
//# sourceMappingURL=PointerDatas.js.map