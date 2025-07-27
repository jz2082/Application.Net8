import Image from 'next/image';
import { useContext } from "react";

import Bids from "@/components/bids/Bids";
import { NavigationContext } from "@/components/App";
import { useFetchHouse, useDeleteHouse } from "@/hooks/HouseHooks";
import defaultImage from "@/helpers/defaultPhoto";
import ApiStatus from "@components/ApiStatus";
import navValues from "@/helpers/navValues";

const HouseDetail = () => {
  const { navigate, param: id } = useContext(NavigationContext);
  const houseId = parseInt(id);
  const { data, status, isSuccess } = useFetchHouse(houseId);
  const deleteHouseMutation = useDeleteHouse();
  
  if (!isSuccess) return <ApiStatus status={status}></ApiStatus>;
  if (!data.data) return <div>House not found.</div>;
  return (
    <div className="row">
      <div className="col-6">
        <div className="row">
          <Image
            className="img-fluid"
            width="200" height="200" 
            src={data.data?.photo ? data.data?.photo : defaultImage}
            alt="House pic"
          />
        </div>
        <div className="row">
          <div className="col-6"></div>
          <div className="col-2">
            <button className="btn btn-primary" onClick={() => navigate(navValues.houseEdit, data.data?.id)}>
            Edit
          </button>
          </div>
          <div className="col-2">
            <button className="btn btn-danger w-100"
              onClick={() => {
                if (window.confirm("Are you sure?"))
                {
                  deleteHouseMutation.mutate(data.data);
                  navigate(navValues.home);
                }
              }}
            >
              Delete
            </button>
          </div>
          <div className="col-2"></div>
        </div>
      </div>
      <div className="col-6">
        <div className="row mt-2">
          <h5 className="col-12">{data.data?.country}</h5>
        </div>
        <div className="row">
          <h3 className="col-12">{data.data?.address}</h3>
        </div>
        <div className="row">
          <h2 className="themeFontColor col-12">
            {data.data?.price}
          </h2>
        </div>
        <div className="row">
          <div className="col-12 mt-3">{data.data?.description}</div>
        </div>
        <Bids houseBid={data.data} />
      </div>
    </div>
  );
};

export default HouseDetail;
