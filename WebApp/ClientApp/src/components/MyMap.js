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
var react_leaflet_1 = require("react-leaflet");
require("./MyMap.css");
var myMApUrl = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}';
var mapToken = 'pk.eyJ1IjoidmFubnlrbyIsImEiOiJja2J1aGw3OTEwNWsxMnJwZ2FueTZmcndpIn0.mQrdpQ4fyJDJX_JvxmNsqQ';
var stamenTonerAttr = 'asdasd';
var mapId = 'mapbox/streets-v11';
var mapCenter = [39.9528, -75.1638];
var zoomLevel = 13;
var MyMapComponent = /** @class */ (function (_super) {
    __extends(MyMapComponent, _super);
    function MyMapComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    MyMapComponent.prototype.render = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement("h1", { id: "tabelLabel" }, "Map of points"),
            React.createElement("p", null, "This component demonstrates all data points on a Leaflet map."),
            this.renderMap()));
    };
    MyMapComponent.prototype.renderMap = function () {
        return (React.createElement("div", { className: "mapdiv" },
            React.createElement(react_leaflet_1.Map, { center: [51.505, -0.09], zoom: zoomLevel, bounds: [[50.505, -29.09], [52.505, 29.09],] },
                React.createElement(react_leaflet_1.TileLayer, { attribution: '&copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors', url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", noWrap: false, tileSize: 512 }),
                React.createElement(react_leaflet_1.Marker, { position: [51.505, -0.09] },
                    React.createElement(react_leaflet_1.Popup, null,
                        "A pretty CSS3 popup. ",
                        React.createElement("br", null),
                        " Easily customizable.")))));
    };
    return MyMapComponent;
}(React.PureComponent));
exports.default = MyMapComponent;
//# sourceMappingURL=MyMap.js.map