import { Link } from "react-router-dom";
import { Breadcrumb,BreadcrumbItem,bre } from "reactstrap";
import { useNavigate } from "react-router-dom";

function Breadcrumbs(crumbs) {
  const navigate = useNavigate();
    return (
<div>
  <Breadcrumb listTag="div">
  {crumbs.crumbs.map((crumb,index) => (
    index + 1 == crumbs.crumbs.length ? (
    <BreadcrumbItem active
      >
          {crumb.Name}
      </BreadcrumbItem>
      ) : (
        <BreadcrumbItem
        href={crumb.link}
        tag="a"
      >
          {console.log(crumbs.Link)}
          {crumb.Name}
          {console.log(index)}
          {console.log(crumbs.crumbs.length)}
          {console.log(crumbs.crumbs)}
      </BreadcrumbItem>
      )
  ))}  
  </Breadcrumb>
</div>
    );
}
export default Breadcrumbs