class HotelsPhotos extends React.Component {
    
    render() {
        return (
            <div class="hotelPhoto" >
                <img src={this.props.photoUrl} class="img" alt="Image" />               
            </div>
        );
    }

}