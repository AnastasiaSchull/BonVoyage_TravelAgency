﻿class ToursInfo extends React.Component {
    constructor(props) {
        super(props);       
    }

    render() {
        return <div>
                 
            {/*<div*/} {/*className={this.state.class1}*/}
                {/*<div class="card mb-4 shadow-sm">*/}
                    <ToursPhotos photoUrl={this.props.tour.photoUrl} />
                    {/*<div class="card-body">*/}
                        {/*<h5 class="card-title">{this.props.tour.title}</h5>*/}
                        {/*<p class="card-text">{this.props.tour.description}</p>*/}
                        {/*<ul class="list-group list-group-flush">*/}
                        {/*    <li class="list-group-item">Duration: {this.props.tour.duration} days</li>*/}
                        {/*    <li class="list-group-item">Price: ${this.props.tour.price}</li>*/}
                        {/*    <li class="list-group-item">Country: {this.props.tour.country}</li>*/}
                        {/*    <li class="list-group-item">Route: {this.props.tour.route}</li>*/}
                        {/*</ul>*/}
                        {/*<div class="card-footer">*/}
                        {/*    <small class="text-muted">Start Date: {this.dateFofmat(this.props.tour.startDate)}</small>*/}
                        {/*    <br />*/}
                        {/*    <small class="text-muted">End Date: {this.dateFofmat(this.props.tour.endDate)}</small>*/}
                        {/*</div>*/}                                   
                {/*</div>   */}  
           </div>            
                   
    }
}
