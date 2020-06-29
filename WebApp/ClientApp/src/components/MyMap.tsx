// @flow

import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';
import { GeoJSON } from 'leaflet';
import * as PointerDatasStore from '../store/PointerDatas';
import './MyMap.css'

const myMApUrl = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}';
const mapToken = 'pk.eyJ1IjoidmFubnlrbyIsImEiOiJja2J1aGw3OTEwNWsxMnJwZ2FueTZmcndpIn0.mQrdpQ4fyJDJX_JvxmNsqQ';
const stamenTonerAttr = 'asdasd';
const mapId = 'mapbox/streets-v11';
const mapCenter = [39.9528, -75.1638];
const zoomLevel = 8;

type PointerDataProps =
    PointerDatasStore.PointerDatasState // ... state we've requested from the Redux store
    & typeof PointerDatasStore.actionCreators // ... plus action creators we've requested

class MyMapComponent extends React.PureComponent<PointerDataProps>
{
    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    // This method is called when the route parameters change
    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    private ensureDataFetched() {
        this.props.requestPointerDatas();
    }

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
            <div id="mapid">
                <Map center={[51.505, -0.09]} zoom={zoomLevel}>
                    <TileLayer attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" noWrap={false} tileSize={512} />
                    {this.props.datas.map((point: PointerDatasStore.PointerData) => 
                        point.east > 0 && point.north > 0 &&
                        <Marker position={GeoJSON.coordsToLatLng([point.east, point.north, point.hight])}>
                                <Popup>
                                    {point.description} <br /> {point.value}
                                </Popup>
                            </Marker>
                    
                    )}   
                    
                </Map>
            </div>
        );
    }
}
export default connect(
    (state: ApplicationState) => state.pointerDatas, // Selects which state properties are merged into the component's props
    PointerDatasStore.actionCreators // Selects which action creators are merged into the component's props
)(MyMapComponent as any);
