import React, { Component } from 'react';

export class Test extends Component {
    static displayName = Test.name;

    constructor(props) {
        super(props);
        this.state = { test_data: [], loading: true };
    }

    componentDidMount() {
        this.GetTestData();
    }

    render() {
        return (
            <div>
                <h1>Test component! And here is your data: {this.state.test_data[0]} {this.state.test_data[1]}</h1>
            </div>
        );
    }

    async GetTestData() {
        const response = await fetch('api/values');
        const data = await response.json();
        this.setState({ test_data: data, loading: false });
    }
}
