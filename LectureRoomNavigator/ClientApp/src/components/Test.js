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
                <h1>Test component! And here is length of your data: {this.state.test_data.length}</h1>
            </div>
        );
    }

    async GetTestData() {
        const response = await fetch('/api/navigationnode');
        const data = await response.json();
        console.log(data);
        this.setState({ test_data: data, loading: false });
    }
}
