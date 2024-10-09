class HotelsInfo extends React.Component {
    constructor(props) {
        super(props);  
        this.backToToursFromHotels = this.backToToursFromHotels.bind(this);
        this.linkToBooking = this.linkToBooking.bind(this);         
    }    

    linkToBooking(tourId) {
        let link = '#'/*'https://localhost:7079/Booking/Create/' + tourId*/;
        return link;
    }

    backToToursFromHotels(e) {
        this.props.click();
    }
    
    render() {
        return <div>            
            <div class="hotel">
                <HotelsPhotos photoUrl={this.props.hotel.photoUrl} />                
                    <h5 class="card-title">{this.props.hotel.name}</h5>
                    <p class="card-text">{this.props.hotel.description}</p>
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item">PricePerNight: ${this.props.hotel.pricePerNight}</li>
                        <li class="list-group-item">Country: {this.props.hotel.country}</li>
                        <li class="list-group-item">City: {this.props.hotel.city}</li>
                        <li class="list-group-item">Location: {this.props.hotel.location}</li>
                    <li class="list-group-item">Swimming pool: {this.props.hotel.hasSwimmingPool}</li>
                    <li class="list-group-item">
                        <button className="nav-link d-inline" style={{ border: "0px", background: "transparent", fontWeight: "bold", fontSize: "16px", width: "160px"}} onClick={this.backToToursFromHotels} >&lt;- Back to tours</button>
                        <a href={this.linkToBooking(this.props.hotel.hotelId)} className="nav-link d-inline" style={{ fontWeight: "bold", color: "orange", fontSize: "16px"}} target="_blank" rel="noopener noreferrer">Book</a> 
                    </li>
                </ul>
            </div>         
         </div>                
    }
}
