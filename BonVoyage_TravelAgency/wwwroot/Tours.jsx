class Tours extends React.Component {
    constructor(props) {
        super(props);
        this.state = { tours: this.props.data.tours};
    }

    render() {
        return(
            <div class="row">
                <h3>{this.props.data.title}</h3>
                {this.state.tours.map(function (tour) {
                    return <ToursInfo tour={tour} />;
                })}
            </div>
        );
    }
}
