import React, { Component } from 'react';
import { MapExample } from "./MapExample";

export class Test extends Component {
    static displayName = Test.name;

    constructor(props) {
        super(props);
        this.state = { test_data: [], loading: true };
        this.m_ = new MapExample();

    }

    componentDidMount() {
        this.GetTestData();
    }

    render() {
        return (
            <div>
                <h1>Test component! And here is length of your data: {this.state.test_data.length}</h1>
                <MapExample />
            </div>
        );
    }

    async GetTestData() {
        const response = await fetch('/api/NavigationNode');
        const data = await response.json();
        console.log(data);
        this.setState({ test_data: data, loading: false });
    }
}
