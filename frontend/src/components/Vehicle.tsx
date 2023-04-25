import React from "react";
import { useEffect, useState } from "react";
import httpModule from "../helpers/http.module";
import { IVehicle } from "../types/global.typing";
import { useAtom } from "jotai";
import { ownerIDAtom } from "../App";

const Vehicle = () => {
  const [vehicles, setVehicles] = useState<IVehicle[]>([]);
  const [ownerID] = useAtom<string>(ownerIDAtom);

  useEffect(() => {
    console.log(ownerID);
    httpModule
      .get<IVehicle[]>(`/Vehicle/Get/${ownerID}`)
      .then((response) => {
        setVehicles(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [ownerID]);

  return (
    <div>
      <ul className="todos">
        {vehicles.map((vehicle) => (
          <li key={vehicle.id}>
            {vehicle.ownerName} {vehicle.make} {vehicle.model} {vehicle.year}{" "}
            {vehicle.vin} {vehicle.serviceRecords}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Vehicle;
