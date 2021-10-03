const Bike = {
    props: ["user"],
    template: `
            <div class="post">
                <table>
                    <thead>
                        <tr>
                            <th>
                            Manufacturer
                            </th>
                            <th>
                            Partname
                            </th>
                            <th>
                            Km
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                            Trek
                            </td>
                            <td>
                            Madone 3.1
                            </td>
                            <td>
                            34500
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
    `,
    data: function() {
        return {
            manufacturer: null
        }
    }
}
