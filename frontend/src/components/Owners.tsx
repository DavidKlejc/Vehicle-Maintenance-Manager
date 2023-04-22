import { useEffect, useState } from "react";
import { IOwner } from "../types/global.typing";
import httpModule from "../helpers/http.module";
import "../styles/Owners.css";
import { Link } from "react-router-dom";
import { useAtom } from "jotai";
import { ownerIDAtom } from "../App";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashCan } from "@fortawesome/free-regular-svg-icons";
import { confirmAlert } from "react-confirm-alert";
import "react-confirm-alert/src/react-confirm-alert.css";
import "../styles/CustomAlert.css";

const Owners = () => {
  const [owners, setOwners] = useState<IOwner[]>([]);
  const [ownerID, setOwnerID] = useAtom(ownerIDAtom);

  useEffect(() => {
    httpModule
      .get<IOwner[]>("/Owner/Get")
      .then((response) => {
        setOwners(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [ownerID]);

  const handleSelectedOwner = (owner_id: string) => {
    setOwnerID(owner_id);
  };

  const handleDeleteOwner = (owner_id: string) => {
    httpModule
      .delete(`/Owner/${owner_id}`)
      .then((response) => {
        setOwners(owners.filter((owner) => owner.id !== owner_id));
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleDeleteOwnerButtonClicked = (owner_id: string) => {
    const options = {
      childrenElement: () => (
        <>
          <h2>Confirm Deletion</h2>
          <div className="alert-message">
            Are you sure you want to delete this owner?
          </div>
        </>
      ),
      buttons: [
        {
          label: "Yes",
          onClick: () => handleDeleteOwner(owner_id),
          className: "confirm-button",
        },
        {
          label: "No",
          className: "cancel-button",
        },
      ],
      closeOnClickOutside: true,
      closeOnEscape: true,
      className: "alert-container",
    };

    confirmAlert(options);
  };

  return (
    <div className="owners">
      <h1>Owners</h1>
      <ul className="owners-list">
        {owners.map((owner) => (
          <li key={owner.id} className="individual-owner">
            <div className="owner-names">
              {owner.firstName} {owner.lastName}
            </div>
            <div className="buttons-wrapper">
              <Link
                onClick={() => handleSelectedOwner(owner.id)}
                to={`/Owner/${owner.id}`}
                className="view-owner-button"
              >
                View
              </Link>
              <FontAwesomeIcon
                icon={faTrashCan}
                className="trash-can-icon"
                onClick={() => handleDeleteOwnerButtonClicked(owner.id)}
              />
            </div>
          </li>
        ))}
      </ul>
      <Link to="/Owner/Create" className="add-owner-button">
        Add Owner
      </Link>
    </div>
  );
};

export default Owners;
