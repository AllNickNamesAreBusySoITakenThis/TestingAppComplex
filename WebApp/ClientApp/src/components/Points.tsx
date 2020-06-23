import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as PointerDatasStore from '../store/PointerDatas';

type PointerDataProps =
    PointerDatasStore.PointerDatasState // ... state we've requested from the Redux store
    & typeof PointerDatasStore.actionCreators // ... plus action creators we've requested
    //& RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters

class Points extends React.PureComponent<PointerDataProps> {
    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    // This method is called when the route parameters change
    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Points</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                {this.renderPointersTable()}                
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestPointerDatas();
    }

    private renderPointersTable() {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                        <th>North</th>
                        <th>East</th>
                        <th>Hight</th>
                        <th>Value</th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.datas.map((point: PointerDatasStore.PointerData) =>
                        <tr key={point.name}>
                            <td>{point.name}</td>
                            <td>{point.description}</td>
                            <td>{point.north}</td>
                            <td>{point.east}</td>
                            <td>{point.hight}</td>
                            <td>{point.value}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    //private renderPagination() {
    //    const prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
    //    const nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

    //    return (
    //        <div className="d-flex justify-content-between">
    //            <Link className='btn btn-outline-secondary btn-sm' to={`/fetch-data/${prevStartDateIndex}`}>Previous</Link>
    //            {this.props.isLoading && <span>Loading...</span>}
    //            <Link className='btn btn-outline-secondary btn-sm' to={`/fetch-data/${nextStartDateIndex}`}>Next</Link>
    //        </div>
    //    );
    //}
}

export default connect(
    (state: ApplicationState) => state.pointerDatas, // Selects which state properties are merged into the component's props
    PointerDatasStore.actionCreators // Selects which action creators are merged into the component's props
)(Points as any);