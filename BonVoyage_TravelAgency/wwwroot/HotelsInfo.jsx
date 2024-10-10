class HotelsInfo extends React.Component {
    constructor(props) {
        super(props);  
        this.backToToursFromHotels = this.backToToursFromHotels.bind(this);
        this.linkToBooking = this.linkToBooking.bind(this);    
        this.swimPool = this.swimPool.bind(this);
        this.rating = this.rating.bind(this);
    }    

    linkToBooking(hotelId) {
        let link = "#" /*'https://localhost:7079/Booking/Create/' + hotelId*/;
        return link;
    }

    backToToursFromHotels(e) {
        this.props.click();
    }

    swimPool(pool) {
        if (pool)
            return 'yes';
        else
           return 'no';
    }

    rating(rating) {
        if (rating == 1)
            return '★';
        if (rating == 2)
            return '★★';
        if (rating == 3)
            return '★★★';
        if (rating == 4)
            return '★★★★';
        if (rating == 5)
            return '★★★★★';
        else
            return '';
    }
    
    render() {
        return <div>            
            <div class="hotel">
                <HotelsPhotos photoUrl={this.props.hotel.photoUrl} />                
                <h4 class="card-title" style={{display: "inline-flex" }}>{this.props.hotel.name}</h4>
                <div class="stars">{this.rating(this.props.hotel.starRating)}</div>
                    <p class="card-text">{this.props.hotel.description}</p>
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item">PricePerNight: ${this.props.hotel.pricePerNight}</li>
                        <li class="list-group-item">Country: {this.props.hotel.country}</li>
                        <li class="list-group-item">City: {this.props.hotel.city}</li>
                        <li class="list-group-item">Location: {this.props.hotel.location}</li>                    
                        <li class="list-group-item">Swimming pool: {this.swimPool(this.props.hotel.hasSwimmingPool)}</li>  
                     
                </ul>
                <div style={{ float: "left", width: "160px" }} >
                    <button className="nav-link d-inline" style={{ border: "0px", background: "transparent", fontWeight: "bold", fontSize: "16px", marginLeft: "10px", paddingTop: "0px" }} onClick={this.backToToursFromHotels} >&lt;- Back to tours</button>
                </div >
                
                <a href={this.linkToBooking(this.props.hotel.hotelId)} className="nav-link d-inline" style={{ fontWeight: "bold", color: "orange", fontSize: "16px", display: "inline-flex", paddingBottom: "0px" }} target="_blank" rel="noopener noreferrer">Book</a>
               
                </div>         
         </div>                
    }
}
