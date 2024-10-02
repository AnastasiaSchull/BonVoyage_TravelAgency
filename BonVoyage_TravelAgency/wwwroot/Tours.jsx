class Tours extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tours: this.props.data.tours, hotels: this.props.data.hotels, id: 0, class: "visible", class1: "nonVisible",
            showingCreateTour: false,
        };
        this.backToTours = this.backToTours.bind(this);
        this.toggleCreateTour = this.toggleCreateTour.bind(this);
    }

    press = value => {
        console.log(value);
        this.setState({ id: value, class: "visible" ? "nonVisible" : "visible", class1: "nonVisible" ? "visible" : "nonVisible" });
    }

    backToTours(e) {
        console.log("change");
        this.setState({ class: "nonVisible" ? "visible" : "nonVisible", class1: "visible" ? "nonVisible" : "visible" });
    }


    toggleCreateTour() {
        console.log("Toggling CreateTour");
        this.setState(prevState => ({
            showingCreateTour: !prevState.showingCreateTour,
        }));
    }
    render() {
        if (this.state.showingCreateTour) {
            console.log("Рендерим CreateTour");
            // только форма создания тура и btn отмены
            return (
                <div>
                    <button className="btn btn-default" onClick={this.toggleCreateTour}>
                        Cancel
                    </button>
                    <CreateTour />
                </div>
            );
        } else {
            return (
                <div>
                    <button className="btn btn-default" onClick={this.toggleCreateTour}>
                        Add New Tour
                    </button>
                    <div className={this.state.class}>

                        <div class="row">
                            <h3>{this.props.data.title}</h3>

                            {this.state.tours.map((tour) => {
                                return (<div class="col-md-4">
                                    <div class="card mb-4 shadow-sm">
                                        <ToursInfo tour={tour}
                                            getId={this.press} />
                                        <button class="btn btn-info" style={{ color: "white", fontWeight: "bold" }} onClick={() => this.press(tour.tourId)} > Tours to {tour.title} -&gt;</button>
                                    </div>
                                </div>);
                            })}
                        </div>
                    </div>
                    <div className={this.state.class1}>
                        <button class="btn btn-info" style={{ color: "white", fontWeight: "bold" }} onClick={this.backToTours} >BACK TO TOURS</button>
                        <br />
                        <br />
                        <div class="row">
                            {
                                this.state.hotels.filter(hotel => hotel.tourId == this.state.id).map(filteredHotel => (
                                    <HotelsInfo hotel={filteredHotel} />
                                ))}
                        </div>
                        <button class="btn btn-info" style={{ color: "white", fontWeight: "bold" }} onClick={this.backToTours} >BACK TO TOURS</button>

                    </div>
                </div>
            );
        }
    }
}