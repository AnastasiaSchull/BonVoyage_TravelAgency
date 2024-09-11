class HotelsInfo extends React.Component {
    constructor(props) {
        super(props);         
    }    
    
    render() {
        return <div class="col-md-4">   
            
            <div class="card mb-4 shadow-sm">
                <HotelsPhotos photoUrl={this.props.hotel.photoUrl} />             
                <div class="card-body">
                    <h5 class="card-title">{this.props.hotel.name}</h5>
                    <p class="card-text">{this.props.hotel.description}</p>
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item">PricePerNight: ${this.props.hotel.pricePerNight}</li>
                        <li class="list-group-item">Country: {this.props.hotel.country}</li>
                        <li class="list-group-item">City: {this.props.hotel.city}</li>
                        <li class="list-group-item">Location: {this.props.hotel.location}</li>
                    </ul>
                    <div class="card-footer">
                        <small class="text-muted">Swimming pool: {this.props.hotel.hasSwimmingPool}</small>               
                    </div>
                    {/*<div>*/}
                    {/*    <a href={this.linkToBooking(this.props.tour.tourId)} className="nav-link d-inline" target="_blank" rel="noopener noreferrer">Book</a>*/}
                    {/*</div>*/}
                </div>
            </div>                   
        </div>                  
    }
}
