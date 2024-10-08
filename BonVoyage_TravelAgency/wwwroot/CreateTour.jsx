class CreateTour extends React.Component {
    constructor(props) {
        super(props);
        this.onAddTour = this.onAddTour.bind(this);
    }

    onAddTour(e) {
        e.preventDefault(); 
        this.props.onSubmit(); // вызываем onSubmit, переданную в props
    }

    render() {
        console.log('Props in CreateTour:', this.props);
        const { tour, onChange, onSubmit } = this.props;

        return (
            <div className="form-container">
                <div className="form-box">
                    <h3>Create New Tour</h3>
                    <form onSubmit={onSubmit}>
                        <label className="form-label">
                            Title:
                            <input
                                type="text"
                                name="title"
                                value={tour.title}
                                onChange={onChange}
                            />
                        </label>
                        <label className="form-label">
                            Description:
                            <textarea
                                name="description"
                                value={tour.description}
                                onChange={onChange}
                            ></textarea>
                        </label>
                        <label className="form-label">
                            Duration (in days):
                            <input
                                type="number"
                                name="duration"
                                value={tour.duration}
                                onChange={onChange}
                            />
                        </label>
                        <label className="form-label">
                            Price:
                            <input
                                type="number"
                                name="price"
                                value={tour.price}
                                onChange={onChange}
                            />
                        </label>
                        <label className="form-label">
                            Country:
                            <input
                                type="text"
                                name="country"
                                value={tour.country}
                                onChange={onChange}
                            />
                        </label>
                        <label className="form-label">
                            Route:
                            <input
                                type="text"
                                name="route"
                                value={tour.route}
                                onChange={onChange}
                            />
                        </label>
                        <label className="form-label">
                            Start Date:
                            <input
                                type="date"
                                name="startDate"
                                value={tour.startDate}
                                onChange={onChange}
                            />
                        </label>
                        <label className="form-label">
                            End Date:
                            <input
                                type="date"
                                name="endDate"
                                value={tour.endDate}
                                onChange={onChange}
                            />
                        </label>
                        <label className="form-label">
                            Tour Photo:
                            <input
                                type="file"
                                name="photoFile"
                                onChange={onChange}
                            />
                        </label>
                        <button className="btn btn-default" type="submit">Create Tour</button>
                    </form>
                </div>
            </div>
        );
    }
}


