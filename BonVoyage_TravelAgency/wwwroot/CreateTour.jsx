class CreateTour extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tour: {
                title: '',
                description: '',
                duration: '',
                price: '',
                country: '',
                route: '',
                startDate: '',
                endDate: '',
                photoFile: null, // файл фотографии
                photoUrl: '' // URL для предпросмотра
            }
        };
    }

    handleChange = (event) => {
        const { name, value, type, files } = event.target;
        if (type === "file") {
            const file = files[0];
            const photoUrl = URL.createObjectURL(file); // генерация URL для предварительного просмотра

            this.setState(prevState => ({
                tour: {
                    ...prevState.tour,
                    photoFile: file, // сохраняем файл в состоянии
                    photoUrl: photoUrl // сохраняем URL для предпросмотра
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

        // добавляем все поля тура в formData
        Object.keys(this.state.tour).forEach(key => {
            if (key === 'photoFile') {
                formData.append('photo', this.state.tour.photoFile); // добавляем файл
            } else {
                formData.append(key, this.state.tour[key]);
            }
        });

        try {
            const response = await fetch('https://localhost:7299/api/Tours', {
                method: 'POST',
                body: formData
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const result = await response.json();
            console.log('Success:', result);
            console.log('Tour Created with Photo URL:', result.photoUrl); // проверка URL фото

            if (this.props.onSave) {
                this.props.onSave(result);  // вызов родительской функции для обновления туров
            }

            // сбрасываем состояние после успешного создания тура
            this.setState({
                tour: {
                    title: '',
                    description: '',
                    duration: '',
                    price: '',
                    country: '',
                    route: '',
                    startDate: '',
                    endDate: '',
                    photoFile: null,
                    photoUrl: '' // сбрасываем URL фотографии
                }
            });

            Swal.fire({
                title: 'Success!',
                text: 'Tour has been successfully created!',
                icon: 'success',
                confirmButtonText: 'Cool'
            });

        } catch (error) {
            console.error('Error:', error);
            Swal.fire({
                title: 'Error!',
                text: 'Failed to create tour.',
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
                    <h3>Create New Tour</h3>
                    <form onSubmit={this.handleSubmit}>
                        <label className="form-label">
                            Title:
                            <input
                                type="text"
                                name="title"
                                value={title}
                                onChange={this.handleChange}
                            />
                        </label >
                        <label className="form-label">
                            Description:
                            <textarea
                                name="description"
                                value={description}
                                onChange={this.handleChange}
                            ></textarea>
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
                            <input type="file" name="photoFile" onChange={this.handleChange} />
                        </label>

                        {/*{photoUrl && <ToursPhotos photoUrl={photoUrl} />}*/}
                        {photoUrl && <img src={photoUrl} className="card-img-top no-hover-photo" alt="Image" />}

                        <button className="btn btn-default" type="submit">Create Tour</button>
                    </form>
                </div>
            </div>
        );
    }
}
