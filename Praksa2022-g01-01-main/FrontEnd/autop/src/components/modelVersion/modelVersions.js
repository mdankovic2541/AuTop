import {useParams} from 'react-router-dom'
import{useState,useEffect} from 'react'
import axios from 'axios';
import{Container,Row,Col,Spinne,Label,Input,Spinner} from 'reactstrap'
import Model from '../model/model.js'
import ModelVersion from './modelVersion.js'
import SearchBar from '../common/searchBar.js';
import '../../App.css'
import Filter from './filter.js';
import Breadcrumbs from '../common/breadCrumbs.js';


function ModelVersions() {

const [modelVersions,setModelVersions] = useState([]);
const [sort, setSort] = useState('ASC')
const [sortby,setSortBy] = useState('Year')
const [search,setSearch] = useState("")
const [filterData,setFilterData] = useState({})
const {modelId} = useParams();

useEffect(() => {
    const Get = async () => {
        await FetchModelVersions();
        }
    Get()
},[sort,search,filterData])

const FetchModelVersions = async () => {
    axios.get('https://localhost:44343/api/ModelVersion/',{params: {
        Name: search,
        ModelId : modelId,
        TransmissionId : filterData.transmission,
        BodyShapeId : filterData.bodyShape,
        PowerFrom: filterData.powerFrom,
        PowerTo: filterData.powerTo,
        YearFrom: filterData.yearFrom,
        YearTo: filterData.yearTo,
        sortby: sortby,
        sortMethod: sort
    
    }}).then((response) => {
        console.log(modelId)
        console.log(response.data);
      setModelVersions(response.data)  
})
}
const sorting = (e) => {
    setSort(e.target.value)
    switch(e.target.value) {
        case 'NASC':
            setSortBy('Name')
            setSort('ASC')
            break;
        case 'NDESC':
            setSortBy('Name')
            setSort('DESC')
            break;
        case 'YASC':
            setSortBy('Year')
            setSort('ASC')
            break;
        case 'YDESC':
            setSortBy('Year')
            setSort('DESC')
            break;
    }
}
const handleClick = (input) => {
    setSearch(input);
}
const onFilterSubmit = (data) => {
    console.log(data)
    setFilterData(data)
}

var crumbs = []
if(modelVersions.length > 0){
crumbs = [
    {"Name" : 'Manufacturers',"Link": '/'},
    {"Name": modelVersions[0].Model.Manufacturer.Name,"Link": '/Manufacturer/' + modelVersions[0].Model.Manufacturer.Id},
    {"Name" : modelVersions[0].Model.Name,"Link": ''}
]
} 

return(
    <Container id='view_con'>
        <Row>
            <Breadcrumbs crumbs={crumbs}/>
        </Row>
        <Row>
            <SearchBar click={handleClick}/>
        </Row>
        
        <Row>
            <Col md='9'>
               {modelVersions.length < 1 ? (<p></p>) : (<h3 id='header'>{modelVersions[0].Model.Manufacturer.Name} {modelVersions[0].Model.Name} versions:</h3>)} 
            </Col>
            <Col md="1">
                    <Label for='sort_select'>Sort</Label>
                </Col>
                <Col md="2">
                    <Input type='select' id='sort_select' name='sort_select' size="sm" onChange={sorting}>
                        <option selected value='NASC'>
                            Name - ASC
                        </option>
                        <option value='NDESC'>
                            Name - DESC
                        </option>
                        <option value='YASC'>
                            Year - ASC
                        </option>
                        <option value='YDESC'>
                            Year - DESC
                        </option>
                    </Input>
                </Col>
        </Row>
        
    {modelVersions == null || modelVersions.lenght < 1 ? (
        <Spinner
        color="primary"
        size=""
      >
        Loading...
      </Spinner>
    ) : (
        <Row>
            <Col md='3'>
            <Filter onFilterSubmit={onFilterSubmit}/>
            </Col>

            <Col md='9'>
                {modelVersions.map((modelVersion) => (
                    <ModelVersion modelVersion = {modelVersion}/>
                ))}
            </Col>
        </Row>
        ) }
</Container>
)
}

export default ModelVersions