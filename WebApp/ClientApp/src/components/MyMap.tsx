// @flow

import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';
import * as PointerDatasStore from '../store/PointerDatas';
import './MyMap.css'

const myMApUrl = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}';
const mapToken = 'pk.eyJ1IjoidmFubnlrbyIsImEiOiJja2J1aGw3OTEwNWsxMnJwZ2FueTZmcndpIn0.mQrdpQ4fyJDJX_JvxmNsqQ';
const stamenTonerAttr = 'asdasd';
const mapId = 'mapbox/streets-v11';
const mapCenter = [39.9528, -75.1638];
const zoomLevel = 13;

type PointerDataProps =
    PointerDatasStore.PointerDatasState // ... state we've requested from the Redux store
    & typeof PointerDatasStore.actionCreators // ... plus action creators we've requested

export default class MyMapComponent extends React.PureComponent<PointerDataProps>
{
    render()
    {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Map of points</h1>
                <p>This component demonstrates all data points on a Leaflet map.</p>
                {this.renderMap()}
            </React.Fragment>
        );
        
    }

    private renderMap()
    {
        
        return (
            <div className="mapdiv">
                <Map center={[51.505, -0.09]} zoom={zoomLevel} bounds={[[50.505, -29.09],[52.505, 29.09],]}>
                    <TileLayer attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" noWrap={false} tileSize={512} />
                    <Marker position={[51.505, -0.09]}>                        
                        <Popup>
                            A pretty CSS3 popup. <br /> Easily customizable.
                        </Popup>
                    </Marker>
                </Map>
            </div>
        );
    }
}
