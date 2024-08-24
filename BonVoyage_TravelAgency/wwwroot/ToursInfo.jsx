class ToursInfo extends React.Component {
    constructor(props) {
        super(props);        
        this.linkToBooking = this.linkToBooking.bind(this);   
        this.dateFofmat = this.dateFofmat.bind(this); 
    }

    linkToBooking(tourId) {
        let link = 'https://localhost:7079/Booking/Create/' + tourId;
        return link;
    }
    dateFofmat(date) {
        let newFormatDate = new Date(date).toLocaleDateString();        
        return newFormatDate;
    }
    
    render() {
        return <div class="col-md-4">
           <div class="card mb-4 shadow-sm">
                <ToursPhotos photoUrl={this.props.tour.photoUrl} />
                <div class="card-body">
                    <h5 class="card-title">{this.props.tour.title}</h5>
                    <p class="card-text">{this.props.tour.description}</p>
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item">Duration: {this.props.tour.duration} days</li>
                        <li class="list-group-item">Price: ${this.props.tour.price}</li>
                        <li class="list-group-item">Country: {this.props.tour.country}</li>
                        <li class="list-group-item">Route: {this.props.tour.route}</li>
                    </ul>
                    <div class="card-footer">
                        <small class="text-muted">Start Date: {this.dateFofmat(this.props.tour.startDate)}</small>
                        <br />
                        <small class="text-muted">End Date: {this.dateFofmat(this.props.tour.endDate)}</small>
                    </div>
                    <div>
                        <a href={this.linkToBooking(this.props.tour.tourId)} className="nav-link d-inline" target="_blank" rel="noopener noreferrer">Book</a>
                    </div>
               </div>
            </div>
        </div>
    }
}
