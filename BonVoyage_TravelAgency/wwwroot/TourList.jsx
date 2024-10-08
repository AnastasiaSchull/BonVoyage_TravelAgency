class TourList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tours: [],
            newTour: {
                title: '',
                description: '',
                duration: '',
                price: '',
                country: '',
                route: '',
                startDate: '',
                endDate: '',
                photoFile: null
            },
            isLoading: false,
            error: null
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }


    componentDidMount() {
        this.fetchTours(); // загрузка списка туров 
    }


    handleChange(event) {
        const { name, value, type, files } = event.target;
        this.setState(prevState => ({
            newTour: {
                ...prevState.newTour,
                [name]: type === "file" ? files[0] : value
            }
        }));
    }


    async handleSubmit(event) {
        event.preventDefault();
        this.setState({ isLoading: true });

        const formData = new FormData();
        Object.entries(this.state.newTour).forEach(([key, value]) => {
            formData.append(key, value);
        });

        try {
            const response = await fetch('https://localhost:7299/api/Tours', {
                method: 'POST',
                body: formData,
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const result = await response.json();
            this.setState({
                tours: [...this.state.tours, result],
                newTour: {
                    title: '',
                    description: '',
                    duration: '',
                    price: '',
                    country: '',
                    route: '',
                    startDate: '',
                    endDate: '',
                    photoFile: null
                },
                isLoading: false
            });
        } catch (error) {
            this.setState({ error, isLoading: false });
        }
    }

    render() {
        const { isLoading, error, newTour } = this.state;  
        return (
            <div>
                <CreateTour
                    tour={newTour}
                    onChange={this.handleChange}
                    onSubmit={this.handleSubmit}
                />               
            </div>
        );
    }
}