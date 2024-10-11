class ToursPhotos extends React.Component {
    
    //render() {
    //    return (
    //        <img src={this.props.photoUrl} class="card-img-top" alt="Image" />
    //    );
    //}

    render() {
        const { photoUrl } = this.props;
        return (
            <img src={photoUrl} class="card-img-top" alt="Image" />
        );
    }

}