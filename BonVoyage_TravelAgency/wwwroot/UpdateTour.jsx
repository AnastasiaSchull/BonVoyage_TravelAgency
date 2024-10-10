class UpdateTour extends React.Component {
    constructor(props) {
        super(props);
      
        this.state = {
            tour: {
                title: props.tour.title || '',
                description: props.tour.description || '',
                duration: props.tour.duration || '',
                price: props.tour.price || '',
                country: props.tour.country || '',
                route: props.tour.route || '',
                startDate: props.tour.startDate ? props.tour.startDate.split('T')[0] : '', // убираем время
                endDate: props.tour.endDate ? props.tour.endDate.split('T')[0] : '',       
                photoUrl: props.tour.photoUrl || ''
            }
        };
    }

    // componentDidUpdate отслеживает изменения пропсов и обновляет состояние, если фото изменилось
    componentDidUpdate(prevProps) {
        if (this.props.tour.photoUrl !== prevProps.tour.photoUrl) {
            this.setState({
                tour: {
                    ...this.state.tour,
                    photoUrl: this.props.tour.photoUrl
                }
            });
        }
    }

    handleChange = (event) => {
        const { name, value, type, files } = event.target;
        if (type === "file") {
            this.setState(prevState => ({
                tour: {
                    ...prevState.tour,
                    photoFile: files[0]
                }
            }));
        } else {
            this.setState(prevState => ({
                tour: {
                    ...prevState.tour,
                    [name]: value
                }
            }));
        }
    };

    handleSubmit = async (event) => {
        event.preventDefault();
        const formData = new FormData();

        // преобразуем дату в формат "yyyy-MM-dd" перед отправкой
        const formattedStartDate = this.state.tour.startDate ? new Date(this.state.tour.startDate).toISOString().split('T')[0] : '';
        const formattedEndDate = this.state.tour.endDate ? new Date(this.state.tour.endDate).toISOString().split('T')[0] : '';
           
        formData.append('TourId', this.props.tour.tourId);
        formData.append('Title', this.state.tour.title);
        formData.append('Description', this.state.tour.description);
        formData.append('Duration', this.state.tour.duration);
        formData.append('Price', this.state.tour.price);
        formData.append('Country', this.state.tour.country);
        formData.append('Route', this.state.tour.route);
        formData.append('StartDate', formattedStartDate);
        formData.append('EndDate', formattedEndDate);

        // добавляем файл, если он был загружен
        if (this.state.tour.photoFile) {
            formData.append('Photo', this.state.tour.photoFile);
        }

        try {
            const response = await fetch(`https://localhost:7299/api/Tours/${this.props.tour.tourId}`, {
                method: 'PUT', // PUT для обновления
                body: formData
                });
            

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const result = await response.json();
            console.log('Success:', result);
           

            // вызываем метод, переданный из родительского компонента, для обновления состояния
           this.props.onSave(result);

            
            Swal.fire({
                title: 'Success!',
                text: 'Tour has been successfully updated!',
                icon: 'success',
                confirmButtonText: 'Cool'
            });

            this.setState({
                tour: {
                    ...this.state.tour,
                    photoUrl: result.photoUrl // добавляем обновленный URL фото из ответа
                }
            });
        } catch (error) {
            console.error('Error:', error);
            Swal.fire({
                title: 'Error!',
                text: 'Failed to update tour.',
                icon: 'error',
                confirmButtonText: 'OK'
            });
        }
       
    };

    render() {
        const { title, description, duration, price, country, route, startDate, endDate, photoUrl } = this.state.tour;
        return (
            <div className="form-container">
                <div className="form-box">
                    <h3>Update Tour</h3>
                    <form onSubmit={this.handleSubmit}>
                        <label className="form-label">
                            Title:
                            <input type="text" name="title" value={title} onChange={this.handleChange} />
                        </label>
                        <label className="form-label">
                            Description:
                            <textarea name="description" value={description} onChange={this.handleChange}></textarea>
                        </label>
                        <label className="form-label">
                            Duration (in days):
                            <input type="number" name="duration" value={duration} onChange={this.handleChange} />
                        </label>
                        <label className="form-label">
                            Price:
                            <input type="number" name="price" value={price} onChange={this.handleChange} />
                        </label>
                        <label className="form-label">
                            Country:
                            <input type="text" name="country" value={country} onChange={this.handleChange} />
                        </label>
                        <label className="form-label">
                            Route:
                            <input type="text" name="route" value={route} onChange={this.handleChange} />
                        </label>
                        <label className="form-label">
                            Start Date:
                            <input type="date" name="startDate" value={startDate} onChange={this.handleChange} />
                        </label>
                        <label className="form-label">
                            End Date:
                            <input type="date" name="endDate" value={endDate} onChange={this.handleChange} />
                        </label>
                        <label className="form-label">
                            Tour Photo:
                            <input type="file" name="photoUrl" onChange={this.handleChange} />
                        </label>
                        <button className="btn btn-default" type="submit">Update Tour</button>
                    </form>
                </div>
            </div>
        );
    }
}
