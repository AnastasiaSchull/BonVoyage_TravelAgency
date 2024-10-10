class Tours extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tours: this.props.data.tours, hotels: this.props.data.hotels, id: 0, class: "visible", class1: "nonVisible",
            showingCreateTour: false,
            showingUpdateTour: false,
            tourToEdit: null,
        };
        this.backToTours = this.backToTours.bind(this);
        this.toggleCreateTour = this.toggleCreateTour.bind(this);
        this.handleDelete = this.handleDelete.bind(this);
        this.handleUpdate = this.handleUpdate.bind(this);
        this.handleSaveUpdatedTour = this.handleSaveUpdatedTour.bind(this);
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

    handleUpdate = (tour) => {
        this.setState({
            showingUpdateTour: true,
            tourToEdit: tour // передаем данные о туре в состояние
        });
    }

    handleSaveUpdatedTour = (updatedTour) => {

        this.setState(prevState => ({
            tours: prevState.tours.map(tour =>
                tour.tourId === updatedTour.tourId ? { ...tour, ...updatedTour } : tour
            ),
            showingUpdateTour: false,  // скрываем форму обновления
            tourToEdit: null           // очищаем выбранный тур для редактирования
        }));
    }

    handleDelete(tourId) {
        console.log("ID of the tour to be deleted:", tourId);
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                // отправляем запрос
                fetch(`https://localhost:7299/api/Tours/${tourId}`, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                    .then(response => {
                        if (response.ok) {
                            Swal.fire(
                                'Deleted!',
                                'Your tour has been deleted.',
                                'success'
                            );
                            // обновляем состояние, удаляя тур из списка
                            this.setState({
                                tours: this.state.tours.filter(tour => tour.tourId !== tourId)
                            });
                        } else {
                            Swal.fire(
                                'Error!',
                                'There was a problem deleting the tour.',
                                'error'
                            );
                        }
                    })
                    .catch(error => {
                        console.error('Error deleting tour:', error);
                        Swal.fire(
                            'Error!',
                            'There was a problem deleting the tour.',
                            'error'
                        );
                    });
            }
        });
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
        }
            // показываем форму обновления тура
        else if (this.state.showingUpdateTour && this.state.tourToEdit) {
            console.log("Рендерим UpdateTour");
            return (
                <div>
                    <button className="btn btn-default" onClick={() => this.setState({ showingUpdateTour: false })}>
                        Cancel Update
                    </button>
                    <UpdateTour tour={this.state.tourToEdit} onSave={this.handleSaveUpdatedTour} />
                </div>
            );
        }

        else {
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

                                        <button
                                            className="btn btn-warning"
                                            style={{ color: "white", fontWeight: "bold" }}
                                            onClick={() => this.handleUpdate(tour)}
                                        >
                                            Update this Tour
                                        </button>

                                        <button
                                            className="btn btn-danger"
                                            style={{ color: "white", fontWeight: "bold" }}
                                            onClick={() => this.handleDelete(tour.tourId)}
                                        >
                                            Delete this Tour                                           
                                        </button>
                                    </div>
                                </div>);
                            })}
                        </div>
                    </div>
                    <div className={this.state.class1}>  
                                                     
                        <br />
                        <div>
                            {
                                this.state.hotels.filter(hotel => hotel.tourId == this.state.id).map(filteredHotel => (
                                    <HotelsInfo hotel={filteredHotel}
                                        click={this.backToTours}/>
                                ))}
                        </div>               
                    </div>
                </div>
            );
        }
    }
}