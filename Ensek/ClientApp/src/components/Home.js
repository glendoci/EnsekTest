import React, { Component } from 'react';
import axios from 'axios';

export class Home extends Component {
    static displayName = Home.name;

    state = {
        selectedFile: null
    }

    

    fileSelectedHandler = event => {
        console.log("FILE ", event.target.files[0]);
        this.setState({
            selectedFile: event.target.files[0]
        })
    }

    fileUploadHandler = async event => {

        const  requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: 'React POST Request Example' })
        }

        const fd = new FormData();
        const response = await fetch('meter-reading-uploads', requestOptions);
        console.log("REP ", response)
        console.log(" GGG ", this.state.selectedFile)
        fd.append('csv', this.state.selectedFile, this.state.selectedFile.name);
        axios.post('http://localhost:7137/meter-reading-uploads', fd).then(res => {
            console.log("REsponse ", res)
        })
    }

    render() {
        return (
            <div>
                <h1>Ensek Stage 2</h1>
                <input type="file" onChange={this.fileSelectedHandler} />
                <button onClick={this.fileUploadHandler}> Upload File </button>
            </div>
        );
    }
}