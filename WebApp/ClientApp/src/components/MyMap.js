"use strict";
// @flow
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
var react_leaflet_1 = require("react-leaflet");
var leaflet_1 = require("leaflet");
var PointerDatasStore = require("../store/PointerDatas");
require("./MyMap.css");
var myMApUrl = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}';
var mapToken = 'pk.eyJ1IjoidmFubnlrbyIsImEiOiJja2J1aGw3OTEwNWsxMnJwZ2FueTZmcndpIn0.mQrdpQ4fyJDJX_JvxmNsqQ';
var stamenTonerAttr = 'asdasd';
var mapId = 'mapbox/streets-v11';
var mapCenter = [39.9528, -75.1638];
var zoomLevel = 8;
var MyMapComponent = /** @class */ (function (_super) {
    __extends(MyMapComponent, _super);
    function MyMapComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    // This method is called when the component is first added to the document
    MyMapComponent.prototype.componentDidMount = function () {
        this.ensureDataFetched();
    };
    // This method is called when the route parameters change
    MyMapComponent.prototype.componentDidUpdate = function () {
        this.ensureDataFetched();
    };
    MyMapComponent.prototype.ensureDataFetched = function () {
        this.props.requestPointerDatas();
    };
    MyMapComponent.prototype.render = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement("h1", { id: "tabelLabel" }, "Map of points"),
            React.createElement("p", null, "This component demonstrates all data points on a Leaflet map."),
            this.renderMap()));
    };
    MyMapComponent.prototype.renderMap = function () {
        return (React.createElement("div", { id: "mapid" },
            React.createElement(react_leaflet_1.Map, { center: [51.505, -0.09], zoom: zoomLevel },
                React.createElement(react_leaflet_1.TileLayer, { attribution: '&copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors', url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", noWrap: false, tileSize: 512 }),
                this.props.datas.map(function (point) {
                    return point.east > 0 && point.north > 0 &&
                        React.createElement(react_leaflet_1.Marker, { position: leaflet_1.GeoJSON.coordsToLatLng([point.east, point.north, point.hight]) },
                            React.createElement(react_leaflet_1.Popup, null,
                                point.description,
                                " ",
                                React.createElement("br", null),
                                " ",
                                point.value));
                }))));
    };
    return MyMapComponent;
}(React.PureComponent));
exports.default = react_redux_1.connect(function (state) { return state.pointerDatas; }, // Selects which state properties are merged into the component's props
PointerDatasStore.actionCreators // Selects which action creators are merged into the component's props
)(MyMapComponent);
//# sourceMappingURL=MyMap.js.map