"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var react_redux_1 = require("react-redux");
var PointerDatasStore = require("../store/PointerDatas");
//& RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters
var Points = /** @class */ (function (_super) {
    __extends(Points, _super);
    function Points() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    // This method is called when the component is first added to the document
    Points.prototype.componentDidMount = function () {
        this.ensureDataFetched();
    };
    // This method is called when the route parameters change
    Points.prototype.componentDidUpdate = function () {
        this.ensureDataFetched();
    };
    Points.prototype.render = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement("h1", { id: "tabelLabel" }, "Points"),
            React.createElement("p", null, "This component demonstrates fetching data from the server and working with URL parameters."),
            this.renderPointersTable()));
    };
    Points.prototype.ensureDataFetched = function () {
        this.props.requestPointerDatas();
    };
    Points.prototype.renderPointersTable = function () {
        return (React.createElement("table", { className: 'table table-striped', "aria-labelledby": "tabelLabel" },
            React.createElement("thead", null,
                React.createElement("tr", null,
                    React.createElement("th", null, "Name"),
                    React.createElement("th", null, "Description"),
                    React.createElement("th", null, "North"),
                    React.createElement("th", null, "East"),
                    React.createElement("th", null, "Hight"),
                    React.createElement("th", null, "Value"))),
            React.createElement("tbody", null, this.props.datas.map(function (point) {
                return React.createElement("tr", { key: point.name },
                    React.createElement("td", null, point.name),
                    React.createElement("td", null, point.description),
                    React.createElement("td", null, point.north),
                    React.createElement("td", null, point.east),
                    React.createElement("td", null, point.hight),
                    React.createElement("td", null, point.value));
            }))));
    };
    return Points;
}(React.PureComponent));
exports.default = react_redux_1.connect(function (state) { return state.pointerDatas; }, // Selects which state properties are merged into the component's props
PointerDatasStore.actionCreators // Selects which action creators are merged into the component's props
)(Points);
//# sourceMappingURL=Points.js.map